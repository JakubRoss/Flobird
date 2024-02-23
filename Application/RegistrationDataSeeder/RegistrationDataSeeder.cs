using Domain.Data.Entities;

namespace Application.RegistrationDataSeeder
{
    internal static class RegistrationDataSeeder
    {
        public static Board RegistrationBoardSeeder( string name, int userId)
        {
            var listCounter = 0;
            var board = new Board()
            {
                Name = name,
                CreatedAt = DateTime.Now,
                Lists = new List<List>()
                {
                    new List()
                    {
                        Name = "To Do",
                        Position = listCounter++,
                        Deadline = DateTime.Today + TimeSpan.FromDays(30),
                        CreatedAt = DateTime.Now,
                        Cards = new List<Card>()
                        {
                            new Card()
                            {
                                Name = "CardOne",
                                Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Vel dolores culpa, molestiae ut blanditiis a rem debitis cupiditate commodi dignissimos aperiam illum cum quaerat, voluptatum ex quia eaque ipsa natus.",
                                CreatedAt = DateTime.Now,
                                Deadline = DateTime.Now + TimeSpan.FromDays(30),
                                Tasks = new List<Tasks>()
                                {
                                    new Tasks()
                                    {
                                        Name = "this.Card first task :)",
                                        CreatedAt = DateTime.Now,
                                        Elements = new List<Element>()
                                        {
                                            new Element()
                                            {
                                                Description = "First element of task is ...",
                                                CreatedAt = DateTime.Now
                                            },
                                            new Element()
                                            {
                                                Description = "Second element of task is...",
                                                CreatedAt = DateTime.Now
                                            },
                                            new Element()
                                            {
                                                Description = "Third element of task is...",
                                                IsComplete = true,
                                                CreatedAt = DateTime.Now
                                            }
                                        }
                                    }
                                },
                                Comments = new List<Comment>()
                                {
                                    new Comment()
                                    {
                                        Text = "My first comment...",
                                        CreatedAt = DateTime.Now,
                                        UserId = userId
                                    },
                                    new Comment()
                                    {
                                        Text = "Second comment...",
                                        CreatedAt = DateTime.Now,
                                        UserId = userId
                                    }
                                },
                                Attachments = new List<Attachment>()
                                {
                                    new Attachment()
                                    {
                                        Name = "Avatar",
                                        Path = "https://chromewebstore.google.com/detail/stw%C3%B3rz-w%C5%82asny-avatar/ofknlbikfofijlcjkfcihomkedmchfbn?hl=pl&pli=1",
                                        DateCreated = DateTime.Now,
                                        UserId = userId
                                    }
                                }
                            }
                        }
                    },
                    new List()
                    {
                        Name = "In Progress",
                        Position = listCounter++,
                        Deadline = DateTime.Today + TimeSpan.FromDays(30),
                        CreatedAt = DateTime.Now
                    },
                    new List()
                    {
                        Name = "Testing",
                        Position = listCounter++,
                        Deadline = DateTime.Today + TimeSpan.FromDays(30),
                        CreatedAt = DateTime.Now
                    },
                    new List()
                    {
                        Name = "Done",
                        Position = listCounter++,
                        Deadline = DateTime.Today + TimeSpan.FromDays(30),
                        CreatedAt = DateTime.Now
                    }
                }
            };
            return board;
        }

        public static string AvatarPathSeeder()
        {
            string url = "https://gitify.net/img/avatars/";

            // Utwórz nowy generator liczb pseudolosowych
            Random random = new Random();

            return $"{url}av-{random.Next(1,18)}.png";
        }
    }
}
