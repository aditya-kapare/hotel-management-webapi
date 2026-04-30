using AutoMapper;
using Contracts;
using DTOs.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Services.Contracts;

namespace Services
{
    public sealed class CabDriverService : ICabDriverService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CabDriverService(
            IRepositoryManager repositoryManager,
            ILoggerManager loggerManager,
            IMapper mapper)
        {
            _repository = repositoryManager;
            _logger = loggerManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CabDriverDTO>> GetAllDrivers(bool trackChanges)
        {
            var drivers = await _repository.CabDriver.GetAllCabDriversAsync(trackChanges);
            var activeDrivers = drivers.Where(d => d.IsActive);
            return _mapper.Map<IEnumerable<CabDriverDTO>>(activeDrivers);
        }

        public async Task<CabDriverDTO> GetDriverById(int driverId, bool trackChanges)
        {
            var driver = await _repository.CabDriver.GetCabDriverByIdAsync(driverId, trackChanges);

            if (driver is null || !driver.IsActive)
                throw new CabDriverNotFoundException(driverId);

            return _mapper.Map<CabDriverDTO>(driver);
        }
        public async Task<CabDriverDTO> GetDriverByGovtId(string govtId, bool trackChanges)
        {
            var driver = await _repository.CabDriver.GetCabDriverByGovtIdAsync(govtId, trackChanges);
            if (driver is null || !driver.IsActive)
                throw new CabDriverNotFoundException(govtId);

            return _mapper.Map<CabDriverDTO>(driver);
        }

        public async Task<CabDriverDTO> CreateDriver(CabDriverForCreationDTO driver)
        {
            var existingDriver = await _repository.CabDriver
                .GetCabDriverByGovtIdAsync(driver.GovernmentId, trackChanges: true);

            if (existingDriver is not null)
            {
                if (existingDriver.IsActive)
                    throw new CabDriverAlreadyExistsException(driver.GovernmentId);

                // restore soft-deleted driver
                _mapper.Map(driver, existingDriver);
                existingDriver.IsActive = true;

                _repository.Save();
                return _mapper.Map<CabDriverDTO>(existingDriver);
            }

            var driverEntity = _mapper.Map<CabDriver>(driver);
            _repository.CabDriver.CreateDriver(driverEntity);
            _repository.Save();

            return _mapper.Map<CabDriverDTO>(driverEntity);
        }

        public async Task UpdateDriver(int driverId, CabDriverForUpdateDTO driverForUpdate, bool trackChanges)
        {
            var driverEntity = await _repository.CabDriver
                .GetCabDriverByIdAsync(driverId, trackChanges);

            if (driverEntity is null || !driverEntity.IsActive)
                throw new CabDriverNotFoundException(driverId);

            _mapper.Map(driverForUpdate, driverEntity);
            _repository.Save();
        }

        public async Task UpdateDriver(string driverId, CabDriverForUpdateDTO driverForUpdate, bool trackChanges)
        {
            var driverEntity = await _repository.CabDriver
                .GetCabDriverByGovtIdAsync(driverId, trackChanges);

            if (driverEntity is null || !driverEntity.IsActive)
                throw new CabDriverNotFoundException(driverId);

            _mapper.Map(driverForUpdate, driverEntity);
            _repository.Save();
        }

        public async Task DeleteDriver(int driverId, bool trackChanges)
        {
            var driver = await _repository.CabDriver
                .GetCabDriverByIdAsync(driverId, trackChanges);

            if (driver is null || !driver.IsActive)
                throw new CabDriverNotFoundException(driverId);

            driver.IsActive = false;
            _repository.CabDriver.UpdateDriver(driver);
            _repository.Save();
        }

        public async Task DeleteDriver(string driverId, bool trackChanges)
        {
            var driver = await _repository.CabDriver
                .GetCabDriverByGovtIdAsync(driverId, trackChanges);

            if (driver is null || !driver.IsActive)
                throw new CabDriverNotFoundException(driverId);

            driver.IsActive = false;
            _repository.CabDriver.UpdateDriver(driver);
            _repository.Save();
        }
    }
}