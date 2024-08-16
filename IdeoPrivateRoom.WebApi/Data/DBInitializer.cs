using IdeoPrivateRoom.WebApi.Data.Entities;
using IdeoPrivateRoom.WebApi.Models.Enums;

namespace IdeoPrivateRoom.WebApi.Data;

public class DBInitializer
{
    public static async void Seed(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

        var users = SeedUsers();
        var roles = SeedRoles();
        var roleMapping = SeedUserRoleMapping(users, roles);
        var vocations = SeedVocations(users);
        var linkedUsers = SeedLinkedUsers(users);
        var userApprovalResponses = SeedUserApprovalResponses(users, vocations);

        if (context == null)
            return;

        if (!context.Users.Any())
        {
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }

        if (!context.Roles.Any())
        {
            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();
        }

        if (!context.UserRoleMappings.Any())
        {
            await context.UserRoleMappings.AddRangeAsync(roleMapping);
            await context.SaveChangesAsync();
        }

        if (!context.VocationRequests.Any())
        {
            await context.VocationRequests.AddRangeAsync(vocations);
            await context.SaveChangesAsync();
        }

        if (!context.LinkedUsers.Any())
        {
            await context.LinkedUsers.AddRangeAsync(linkedUsers);
            await context.SaveChangesAsync();
        }

        if (!context.UserApprovalResponses.Any())
        {
            await context.UserApprovalResponses.AddRangeAsync(userApprovalResponses);
            await context.SaveChangesAsync();
        }
    }

    private static List<Role> SeedRoles()
    {
        return
        [
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "Developer",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "DevOps",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "QA",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "Product Manager",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "UI/UX Designer",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "HR Manager",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "Team Lead",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            }
        ];
    }

    private static List<User> SeedUsers()
    {
        return
        [
            new User
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
            new User
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
            new User
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
            new User
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
            new User
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
            new User
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
            new User
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
            new User
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

    private static List<UserRoleMapping> SeedUserRoleMapping(List<User> users, List<Role> roles)
    {
        return
        [
            new UserRoleMapping
            {
                UserId = users[0].Id,
                User = users[0],
                RoleId = roles[0].Id,
                Role = roles[0],
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserRoleMapping
            {
                UserId = users[0].Id,
                User = users[0],
                RoleId = roles[6].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },

            new UserRoleMapping
            {
                UserId = users[1].Id,
                User = users[1],
                RoleId = roles[5].Id,
                Role = roles[5],
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },

            new UserRoleMapping
            {
                UserId = users[2].Id,
                User = users[2],
                RoleId = roles[2].Id,
                Role = roles[2],
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },

            new UserRoleMapping
            {
                UserId = users[3].Id,
                User = users[3],
                RoleId = roles[1].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },

            new UserRoleMapping
            {
                UserId = users[4].Id,
                User = users[4],
                RoleId = roles[3].Id,
                Role = roles[3],
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },

            new UserRoleMapping
            {
                UserId = users[5].Id,
                User = users[5],
                RoleId = roles[4].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },

            new UserRoleMapping
            {
                UserId = users[6].Id,
                User = users[6],
                RoleId = roles[0].Id,
                Role = roles[0],
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserRoleMapping
            {
                UserId = users[6].Id,
                User = users[6],
                RoleId = roles[6].Id,
                Role = roles[6],
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },

            new UserRoleMapping
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

    private static List<VocationRequest> SeedVocations(List<User> users)
    {
        return
        [
            new VocationRequest
            {
                Id = Guid.NewGuid(),
                UserId = users[0].Id,
                User = users[0],
                StartDate = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 9, 14, 0, 0, 0, DateTimeKind.Utc),
                Title = "John Doe - Vocation",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                VocationStatus = ApprovalStatus.Pending
            },
            new VocationRequest
            {
                Id = Guid.NewGuid(),
                UserId = users[1].Id,
                User = users[1],
                StartDate = new DateTime(2024, 10, 15, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 10, 22, 0, 0, 0, DateTimeKind.Utc),
                Title = "Jane Smith - Vocation",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                VocationStatus = ApprovalStatus.Rejected
            },
            new VocationRequest
            {
                Id = Guid.NewGuid(),
                UserId = users[2].Id,
                User = users[2],
                StartDate = new DateTime(2024, 11, 5, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 11, 6, 0, 0, 0, DateTimeKind.Utc),
                Title = "Alice Jones - Day Off",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                VocationStatus = ApprovalStatus.Approved
            },
            new VocationRequest
            {
                Id = Guid.NewGuid(),
                UserId = users[3].Id,
                User = users[3],
                StartDate = new DateTime(2024, 11, 10, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 11, 19, 0, 0, 0, DateTimeKind.Utc),
                Title = "Bob Brown - Vocation",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                VocationStatus = ApprovalStatus.Approved
            }
        ];
    }

    private static List<LinkedUser> SeedLinkedUsers(List<User> users)
    {
        return
        [
            new LinkedUser
            {
                UserId = users[0].Id,
                LinkedUserId = users[6].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUser
            {
                UserId = users[0].Id,
                LinkedUserId = users[7].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUser
            {
                UserId = users[2].Id,
                LinkedUserId = users[3].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUser
            {
                UserId = users[4].Id,
                LinkedUserId = users[6].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new LinkedUser
            {
                UserId = users[4].Id,
                LinkedUserId = users[7].Id,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            }
        ];
    }
    private static List<UserApprovalResponse> SeedUserApprovalResponses(List<User> users, List<VocationRequest> vocations)
    {
        return
        [
            new UserApprovalResponse
            {
                VocationRequestId = vocations[0].Id,
                UserId = users[6].Id,
                ApprovalStatus = ApprovalStatus.Approved,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserApprovalResponse
            {
                VocationRequestId = vocations[0].Id,
                UserId = users[7].Id,
                ApprovalStatus = ApprovalStatus.Rejected,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserApprovalResponse
            {
                VocationRequestId = vocations[2].Id,
                UserId = users[6].Id,
                ApprovalStatus = ApprovalStatus.Approved,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new UserApprovalResponse
            {
                VocationRequestId = vocations[3].Id,
                UserId = users[6].Id,
                ApprovalStatus = ApprovalStatus.Approved,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            }
        ];
    }
}
