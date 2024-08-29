using IdeoPrivateRoom.DAL.Data.Entities;

namespace IdeoPrivateRoom.DAL.Data;

public class DBInitializer
{
    public static void Seed(ApplicationDbContext? context)
    {
        var users = SeedUsers();
        var roles = SeedRoles();
        var roleMapping = SeedUserRoleMapping(users, roles);
        var vacations = SeedVacations(users);
        var linkedUsers = SeedLinkedUsers(users);
        var userApprovalResponses = SeedUserApprovalResponses(users, vacations);

        if (context == null)
            return;

        if (!context.Users.Any())
        {
            context.Users.AddRange(users);
            context.SaveChanges();
        }

        if (!context.Roles.Any())
        {
            context.Roles.AddRange(roles);
            context.SaveChanges();
        }

        if (!context.UserRoleMappings.Any())
        {
            context.UserRoleMappings.AddRange(roleMapping);
            context.SaveChanges();
        }

        if (!context.VacationRequests.Any())
        {
            context.VacationRequests.AddRange(vacations);
            context.SaveChanges();
        }

        if (!context.LinkedUsers.Any())
        {
            context.LinkedUsers.AddRange(linkedUsers);
            context.SaveChanges();
        }

        if (!context.UserApprovalResponses.Any())
        {
            context.UserApprovalResponses.AddRange(userApprovalResponses);
            context.SaveChanges();
        }
    }

    private static List<RoleEntity> SeedRoles()
    {
        return
        [
            new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = "Developer",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = "DevOps",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = "QA",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = "Product Manager",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = "UI/UX Designer",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = "HR Manager",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = "Team Lead",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            }
        ];
    }

    private static List<UserEntity> SeedUsers()
    {
        return
        [
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = "john.doe@example.com",
                FirstName = "John",
                LastName = "Doe",
                UserIcon = "https://cdn-icons-png.flaticon.com/512/9187/9187604.png",
                PasswordHash = "hashedpassword1",
                IsEmailConfirmed = true,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = "jane.smith@example.com",
                FirstName = "Jane",
                LastName = "Smith",
                UserIcon = "https://cdn-icons-png.flaticon.com/512/9187/9187604.png",
                PasswordHash = "hashedpassword2",
                IsEmailConfirmed = true,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = "alice.jones@example.com",
                FirstName = "Alice",
                LastName = "Jones",
                UserIcon = "https://cdn-icons-png.flaticon.com/512/9187/9187604.png",
                PasswordHash = "hashedpassword3",
                IsEmailConfirmed = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = "bob.brown@example.com",
                FirstName = "Bob",
                LastName = "Brown",
                UserIcon = "https://cdn-icons-png.flaticon.com/512/9187/9187604.png",
                PasswordHash = "hashedpassword4",
                IsEmailConfirmed = true,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = "charlie.miller@example.com",
                FirstName = "Charlie",
                LastName = "Miller",
                UserIcon = "https://cdn-icons-png.flaticon.com/512/9187/9187604.png",
                PasswordHash = "hashedpassword5",
                IsEmailConfirmed = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = "diana.wilson@example.com",
                FirstName = "Diana",
                LastName = "Wilson",
                UserIcon = "https://cdn-icons-png.flaticon.com/512/9187/9187604.png",
                PasswordHash = "hashedpassword6",
                IsEmailConfirmed = true,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = "edward.davis@example.com",
                FirstName = "Edward",
                LastName = "Davis",
                UserIcon = "https://cdn-icons-png.flaticon.com/512/9187/9187604.png",
                PasswordHash = "hashedpassword7",
                IsEmailConfirmed = true,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = "fiona.moore@example.com",
                FirstName = "Fiona",
                LastName = "Moore",
                UserIcon = "https://cdn-icons-png.flaticon.com/512/9187/9187604.png",
                PasswordHash = "hashedpassword8",
                IsEmailConfirmed = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            }
        ];
    }

    private static List<UserRoleMappingEntity> SeedUserRoleMapping(List<UserEntity> users, List<RoleEntity> roles)
    {
        return
        [
            new UserRoleMappingEntity
            {
                UserId = users[0].Id,
                User = users[0],
                RoleId = roles[0].Id,
                Role = roles[0],
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserRoleMappingEntity
            {
                UserId = users[0].Id,
                User = users[0],
                RoleId = roles[6].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },

            new UserRoleMappingEntity
            {
                UserId = users[1].Id,
                User = users[1],
                RoleId = roles[5].Id,
                Role = roles[5],
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },

            new UserRoleMappingEntity
            {
                UserId = users[2].Id,
                User = users[2],
                RoleId = roles[2].Id,
                Role = roles[2],
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },

            new UserRoleMappingEntity
            {
                UserId = users[3].Id,
                User = users[3],
                RoleId = roles[1].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },

            new UserRoleMappingEntity
            {
                UserId = users[4].Id,
                User = users[4],
                RoleId = roles[3].Id,
                Role = roles[3],
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },

            new UserRoleMappingEntity
            {
                UserId = users[5].Id,
                User = users[5],
                RoleId = roles[4].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },

            new UserRoleMappingEntity
            {
                UserId = users[6].Id,
                User = users[6],
                RoleId = roles[0].Id,
                Role = roles[0],
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserRoleMappingEntity
            {
                UserId = users[6].Id,
                User = users[6],
                RoleId = roles[6].Id,
                Role = roles[6],
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },

            new UserRoleMappingEntity
            {
                UserId = users[7].Id,
                User = users[7],
                RoleId = roles[5].Id,
                Role = roles[5],
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            }
        ];
    }

    private static List<VacationRequestEntity> SeedVacations(List<UserEntity> users)
    {
        return
        [
            new VacationRequestEntity
            {
                Id = Guid.NewGuid(),
                UserId = users[0].Id,
                User = users[0],
                StartDate = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 9, 14, 0, 0, 0, DateTimeKind.Utc),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                VacationStatus = "2"
            },
            new VacationRequestEntity
            {
                Id = Guid.NewGuid(),
                UserId = users[1].Id,
                User = users[1],
                StartDate = new DateTime(2024, 10, 15, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 10, 22, 0, 0, 0, DateTimeKind.Utc),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                VacationStatus = "0"
            },
            new VacationRequestEntity
            {
                Id = Guid.NewGuid(),
                UserId = users[2].Id,
                User = users[2],
                StartDate = new DateTime(2024, 11, 5, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 11, 6, 0, 0, 0, DateTimeKind.Utc),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                VacationStatus = "0"
            },
            new VacationRequestEntity
            {
                Id = Guid.NewGuid(),
                UserId = users[3].Id,
                User = users[3],
                StartDate = new DateTime(2024, 11, 10, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 11, 19, 0, 0, 0, DateTimeKind.Utc),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                VacationStatus = "1"
            },
            new VacationRequestEntity
            {
                Id = Guid.NewGuid(),
                UserId = users[4].Id,
                User = users[4],
                StartDate = new DateTime(2024, 12, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 12, 7, 0, 0, 0, DateTimeKind.Utc),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                VacationStatus = "0"
            },
            new VacationRequestEntity
            {
                Id = Guid.NewGuid(),
                UserId = users[5].Id,
                User = users[5],
                StartDate = new DateTime(2025, 1, 10, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                VacationStatus = "0"
            },
            new VacationRequestEntity
            {
                Id = Guid.NewGuid(),
                UserId = users[6].Id,
                User = users[6],
                StartDate = new DateTime(2025, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2025, 2, 5, 0, 0, 0, DateTimeKind.Utc),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                VacationStatus = "0"
            }
        ];
    }

    private static List<LinkedUserEntity> SeedLinkedUsers(List<UserEntity> users)
    {
        return
        [
            new LinkedUserEntity
            {
                UserId = users[0].Id,
                LinkedUserId = users[6].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUserEntity
            {
                UserId = users[0].Id,
                LinkedUserId = users[7].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUserEntity
            {
                UserId = users[2].Id,
                LinkedUserId = users[3].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUserEntity
            {
                UserId = users[4].Id,
                LinkedUserId = users[6].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUserEntity
            {
                UserId = users[4].Id,
                LinkedUserId = users[7].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUserEntity
            {
                UserId = users[1].Id,
                LinkedUserId = users[5].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUserEntity
            {
                UserId = users[1].Id,
                LinkedUserId = users[6].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUserEntity
            {
                UserId = users[2].Id,
                LinkedUserId = users[4].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUserEntity
            {
                UserId = users[3].Id,
                LinkedUserId = users[7].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUserEntity
            {
                UserId = users[5].Id,
                LinkedUserId = users[7].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUserEntity
            {
                UserId = users[5].Id,
                LinkedUserId = users[6].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUserEntity
            {
                UserId = users[5].Id,
                LinkedUserId = users[2].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUserEntity
            {
                UserId = users[6].Id,
                LinkedUserId = users[5].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUserEntity
            {
                UserId = users[6].Id,
                LinkedUserId = users[2].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
        ];
    }
    private static List<UserApprovalResponseEntity> SeedUserApprovalResponses(List<UserEntity> users, List<VacationRequestEntity> vacations)
    {
        return
        [
            new UserApprovalResponseEntity
            {
                VacationRequestId = vacations[0].Id,
                UserId = users[6].Id,
                ApprovalStatus = "1",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserApprovalResponseEntity
            {
                VacationRequestId = vacations[3].Id,
                UserId = users[7].Id,
                ApprovalStatus = "1",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserApprovalResponseEntity
            {
                VacationRequestId = vacations[6].Id,
                UserId = users[2].Id,
                ApprovalStatus = "1",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserApprovalResponseEntity
            {
                VacationRequestId = vacations[1].Id,
                UserId = users[1].Id,
                ApprovalStatus = "2",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserApprovalResponseEntity
            {
                VacationRequestId = vacations[0].Id,
                UserId = users[7].Id,
                ApprovalStatus = "2",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserApprovalResponseEntity
            {
                VacationRequestId = vacations[2].Id,
                UserId = users[6].Id,
                ApprovalStatus = "1",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserApprovalResponseEntity
            {
                VacationRequestId = vacations[3].Id,
                UserId = users[6].Id,
                ApprovalStatus = "1",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            }
        ];
    }
}
