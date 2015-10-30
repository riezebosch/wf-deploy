using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConDep.implementation.Persistence
{
    public class Workflow
    {
        [Key]
        public int Id { get; set; }
        public string FileLocation { get; set; }
        public string Filename { get; set; }
    }
}