using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataSeeder
{
    public class RoomsSeeder : ISeeder
    { 
        public async Task SeedAsync(RepositoryContext db, CancellationToken cancellationToken = default)
        {
            if (await db.Rooms.AnyAsync(cancellationToken))
                return;

            var rooms = new List<Room>
            {

                new Room {RoomNo=101, DoesExist = true, RoomType = RoomType.SingleBed, AcOption = AcOption.NonAC, AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 1200m },
                new Room {RoomNo=102, DoesExist = true, RoomType = RoomType.SingleBed, AcOption = AcOption.AC,    AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 1500m },
                new Room {RoomNo=103, DoesExist = true, RoomType = RoomType.SingleBed, AcOption = AcOption.NonAC, AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 1200m },
                new Room {RoomNo=104, DoesExist = true, RoomType = RoomType.SingleBed, AcOption = AcOption.AC,    AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 1500m },


                new Room {RoomNo=106, DoesExist = true, RoomType = RoomType.DoubleBed, AcOption = AcOption.NonAC, AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 1800m },
                new Room {RoomNo=107, DoesExist = true, RoomType = RoomType.DoubleBed, AcOption = AcOption.AC,    AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 2200m },
                new Room {RoomNo=108, DoesExist = true, RoomType = RoomType.DoubleBed, AcOption = AcOption.AC,    AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 2200m },
                new Room {RoomNo=109, DoesExist = true, RoomType = RoomType.DoubleBed, AcOption = AcOption.NonAC, AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 1800m },

                new Room {RoomNo=111, DoesExist = true, RoomType = RoomType.DoubleBed, AcOption = AcOption.AC,    AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 2300m },
                new Room {RoomNo=112, DoesExist = true, RoomType = RoomType.DoubleBed, AcOption = AcOption.NonAC, AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 1900m },
                new Room {RoomNo=113, DoesExist = true, RoomType = RoomType.DoubleBed, AcOption = AcOption.AC,    AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 2300m },
                new Room {RoomNo=114, DoesExist = true, RoomType = RoomType.DoubleBed, AcOption = AcOption.NonAC, AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 1900m },

                new Room {RoomNo=116, DoesExist = true, RoomType = RoomType.SemiDeluxe, AcOption = AcOption.AC,    AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 2800m },
                new Room {RoomNo=117, DoesExist = true, RoomType = RoomType.SemiDeluxe, AcOption = AcOption.AC,    AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 3000m },
                new Room {RoomNo=118, DoesExist = true, RoomType = RoomType.SemiDeluxe, AcOption = AcOption.NonAC, AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 2500m },
                new Room {RoomNo=119, DoesExist = true, RoomType = RoomType.SemiDeluxe, AcOption = AcOption.AC,    AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 3000m },
                new Room {RoomNo=120, DoesExist = true, RoomType = RoomType.SemiDeluxe, AcOption = AcOption.NonAC, AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 2600m },


                new Room {RoomNo=105, RoomType = RoomType.Deluxe, AcOption = AcOption.AC, AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 4500m },
                new Room {RoomNo=110, RoomType = RoomType.Deluxe, AcOption = AcOption.AC, AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 4800m },
                new Room {RoomNo=115, RoomType = RoomType.Deluxe, AcOption = AcOption.AC, AvailabilityStatus = AvailabilityStatus.Available, CleanStatus = CleanStatus.Clean, Price = 5000m }
            };

            await db.Rooms.AddRangeAsync(rooms, cancellationToken);
        }
    }
}
