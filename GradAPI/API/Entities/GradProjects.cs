using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class GradProjects
    {
      public int Id { get; set; }
      public int GradId { get; set; }
      public Grads Grad { get; set; }

      public int ProjectsId { get; set; }
      public Projects Projects { get; set; }

      public int Duration { get; set; }
    }
}