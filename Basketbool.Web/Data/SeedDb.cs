
using Basketbool.Web.Data.Entities;
using Basketbool.Web.Enums;
using Basketbool.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basketbool.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckTeamsAsync();
            await CheckSeasonsAsync();
            await CheckRolesAsync();
            await CheckUserAsync("99999999", "Fabio", "Cano", "fabio@gmail.com", "3256548952", "cr 56 7895", UserType.Admin);
        }

        private async Task<UserEntity> CheckUserAsync(
                                string document,
                                string firstName,
                                string lastName,
                                string email,
                                string phone,
                                string address,
                                UserType userType)
        {
            UserEntity user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    UserType = userType,
                };
                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

            }
            return user;
        }
        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(nameof(UserType.Admin));
            await _userHelper.CheckRoleAsync(nameof(UserType.User));
        }

        private async Task CheckSeasonsAsync()
        {
            if (!_context.Seasons.Any())
            {
                DateTime startDate = DateTime.Today.AddMonths(2).ToUniversalTime();
                DateTime endDate = DateTime.Today.AddMonths(3).ToUniversalTime();

                _context.Seasons.Add(new SeasonEntity
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    IsActive = true,
                    Name = "1",
                    MatchDays = new List<MatchDayEntity>
                    {
                        new MatchDayEntity
                        {
                             Name = "1.1",
                             MatchDayDetails = new List<MatchDayDetailEntity>
                             {
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Miami Heat") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Washington Wizards") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Houston Rockets") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Indiana Pacers") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Miami Heat"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Washington Wizards")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Houston Rockets"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Indiana Pacers")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(4).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Miami Heat"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Houston Rockets")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(4).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Washington Wizards"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Indiana Pacers")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(9).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Indiana Pacers"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Miami Heat")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(9).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Washington Wizards"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Houston Rockets")
                                 }
                             }
                        },
                        new MatchDayEntity
                        {
                             Name = "1.3",
                             MatchDayDetails = new List<MatchDayDetailEntity>
                             {
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Chicago Bulls") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Memphis Grizzlies") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Los Ángeles Lakers") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == " Charlotte Hornets") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Chicago Bulls"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Memphis Grizzlies")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Los Ángeles Lakers"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == " Charlotte Hornets")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Chicago Bulls"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Los Ángeles Lakers")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Memphis Grizzlies"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == " Charlotte Hornets")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(10).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == " Charlotte Hornets"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Chicago Bulls")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(10).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Memphis Grizzlies"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Los Ángeles Lakers")
                                 }
                             }
                        },
                        new MatchDayEntity
                        {
                             Name = "1.4",
                             MatchDayDetails = new List<MatchDayDetailEntity>
                             {
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Cleveland Cavaliers,") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Dallas Mavericks") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Houston Rockets") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "New Orleans Pelicans") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(2).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Cleveland Cavaliers,"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Dallas Mavericks")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(2).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Houston Rockets"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "New Orleans Pelicans")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(6).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Cleveland Cavaliers,"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Houston Rockets")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(6).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Dallas Mavericks"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "New Orleans Pelicans")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(11).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "New Orleans Pelicans"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Cleveland Cavaliers,")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(11).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Dallas Mavericks"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Houston Rockets")
                                 }
                             }
                        },
                        new MatchDayEntity
                        {
                             Name = "1.5",
                             MatchDayDetails = new List<MatchDayDetailEntity>
                             {
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Sacramento Kings") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Philadelphia 76ers") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Orlando Magic") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Minnesota Timberwolves") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(3).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Sacramento Kings"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Philadelphia 76ers")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(3).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Orlando Magic"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Minnesota Timberwolves")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(7).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Sacramento Kings"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Orlando Magic")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(7).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Philadelphia 76ers"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Minnesota Timberwolves")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Minnesota Timberwolves"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Sacramento Kings")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Philadelphia 76ers"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Orlando Magic")
                                 }
                             }
                        }
                    }
                });

                startDate = DateTime.Today.AddMonths(1).ToUniversalTime();
                endDate = DateTime.Today.AddMonths(4).ToUniversalTime();

                _context.Seasons.Add(new SeasonEntity
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    IsActive = true,
                    Name = "2",
                    MatchDays = new List<MatchDayEntity>
                    {
                        new MatchDayEntity
                        {
                             Name = "2.1",
                             MatchDayDetails = new List<MatchDayDetailEntity>
                             {
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Brooklyn Nets") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Detroit Pistons") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Portland Trail Blazers") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Los Ángeles Clippers") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Brooklyn Nets"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Detroit Pistons")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Portland Trail Blazers"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Los Ángeles Clippers")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(4).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Brooklyn Nets"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Portland Trail Blazers")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(4).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Detroit Pistons"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Los Ángeles Clippers")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(9).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Los Ángeles Clippers"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Brooklyn Nets")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(9).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Detroit Pistons"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Portland Trail Blazers")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(15).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Detroit Pistons"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Brooklyn Nets")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(15).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Los Ángeles Clippers"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Portland Trail Blazers")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(19).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Portland Trail Blazers"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Brooklyn Nets")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(19).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Los Ángeles Clippers"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Detroit Pistons")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(19).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Brooklyn Nets"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Los Ángeles Clippers")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(19).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Portland Trail Blazers"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Detroit Pistons")
                                 }
                             }
                        },
                        new MatchDayEntity
                        {
                             Name = "2.2",
                             MatchDayDetails = new List<MatchDayDetailEntity>
                             {
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Phoenix Suns") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Sacramento Kings") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Dallas Mavericks") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Utah Jazz") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Phoenix Suns"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Sacramento Kings")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Dallas Mavericks"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Utah Jazz")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Phoenix Suns"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Dallas Mavericks")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Sacramento Kings"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Utah Jazz")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(10).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Utah Jazz"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Phoenix Suns")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(10).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Sacramento Kings"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Dallas Mavericks")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(16).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Sacramento Kings"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Phoenix Suns")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(16).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Utah Jazz"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Dallas Mavericks")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(20).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Dallas Mavericks"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Phoenix Suns")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(20).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Utah Jazz"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Sacramento Kings")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(35).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Phoenix Suns"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Utah Jazz")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(35).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Dallas Mavericks"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Sacramento Kings")
                                 }
                             }
                        }
                    }
                });

                startDate = DateTime.Today.AddMonths(1).ToUniversalTime();
                endDate = DateTime.Today.AddMonths(2).ToUniversalTime();

                _context.Seasons.Add(new SeasonEntity
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    IsActive = true,
                    Name = "3",
                    MatchDays = new List<MatchDayEntity>
                    {
                        new MatchDayEntity
                        {
                             Name = "3.1",
                             MatchDayDetails = new List<MatchDayDetailEntity>
                             {
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Celtics") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Atlanta Hawks") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Celtics"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Atlanta Hawks")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Atlanta Hawks"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Celtics")
                                 }
                             }
                        },
                        new MatchDayEntity
                        {
                             Name = "3.2",
                             MatchDayDetails = new List<MatchDayDetailEntity>
                             {
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "New York Knicks") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Milwaukee Bucks.") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "New York Knicks"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Milwaukee Bucks.")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Milwaukee Bucks."),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "New York Knicks")
                                 }
                             }
                        },
                        new MatchDayEntity
                        {
                             Name = "3.3",
                             MatchDayDetails = new List<MatchDayDetailEntity>
                             {
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Toronto Raptors.") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Oklahoma City Thunder") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Toronto Raptors."),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Oklahoma City Thunder")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Oklahoma City Thunder"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Toronto Raptors.")
                                 }
                             }
                        },
                        new MatchDayEntity
                        {
                             Name = "3.4",
                             MatchDayDetails = new List<MatchDayDetailEntity>
                             {
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == " San Antonio Spurs") },
                                 new MatchDayDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Golden State Warriors") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == " San Antonio Spurs"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Golden State Warriors")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Golden State Warriors"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == " San Antonio Spurs")
                                 }
                             }
                        }
                    }
                });


                await _context.SaveChangesAsync();
            }
        }


        private async Task CheckTeamsAsync()
        {
            if (!_context.Teams.Any())
            {
                AddTeam("Celtics");
                AddTeam("Brooklyn Nets");
                AddTeam("Chicago Bulls");
                AddTeam("Atlanta Hawks");
                AddTeam("New York Knicks");
                AddTeam("Philadelphia 76ers");
                AddTeam("Toronto Raptors.");
                AddTeam("Cleveland Cavaliers,");
                AddTeam("Detroit Pistons");
                AddTeam("Indiana Pacers");
                AddTeam("Milwaukee Bucks.");
                AddTeam(" Charlotte Hornets");
                AddTeam("Miami Heat");
                AddTeam("Orlando Magic");
                AddTeam("Washington Wizards");
                AddTeam("Minnesota Timberwolves");
                AddTeam("Oklahoma City Thunder");
                AddTeam("Portland Trail Blazers");
                AddTeam("Utah Jazz");
                AddTeam("Golden State Warriors");
                AddTeam("Los Ángeles Clippers");
                AddTeam("Los Ángeles Lakers");
                AddTeam("Phoenix Suns");
                AddTeam("Sacramento Kings");
                AddTeam("Dallas Mavericks");
                AddTeam("Houston Rockets");
                AddTeam("Memphis Grizzlies");
                AddTeam("New Orleans Pelicans");
                AddTeam(" San Antonio Spurs");

                await _context.SaveChangesAsync();
            }
        }

        private void AddTeam(string name)
        {
            _context.Teams.Add(new TeamEntity { Name = name });
        }


    }
}
