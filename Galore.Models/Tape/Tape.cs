using System;

namespace Galore.Models.Tape {
    public class Tape {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DirectorFirstName { get; set; }
        public string DirectorLastName { get; set; }
        public string Type { get; set; }
        public string EIDR { get; set; }
        public bool Deleted { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}