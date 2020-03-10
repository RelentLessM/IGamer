namespace IGamer.Data.Models
{
    using System;
    using System.Collections.Generic;

    using IGamer.Data.Common.Models;

    public class Game : BaseModel<string>
    {
        public Game()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Guides = new HashSet<Guide>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Guide> Guides { get; set; }
    }
}