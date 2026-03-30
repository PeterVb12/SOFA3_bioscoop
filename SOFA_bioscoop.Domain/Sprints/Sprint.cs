using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA_bioscoop.Domain
{
    public class Sprint
    {
        private string name;
        private DateTime startDate;
        private DateTime endDate;

        private List<BacklogItem> sprintBacklog = new List<BacklogItem>();
        private ISprintState state;
        private ISprintTypeStrategy strategy;
        private Project linkedProject;

        public Sprint(string name, 
                      DateTime startDate, 
                      DateTime endDate, 
                      List<BacklogItem> sprintBacklog, 
                      ISprintTypeStrategy strategy, 
                      Project linkedProject)
        {
            this.name = name;
            this.startDate = startDate;
            this.endDate = endDate;
            this.sprintBacklog = sprintBacklog;
            this.state = new CreatedState();
            this.strategy = strategy;
            this.linkedProject = linkedProject;
        }
    }
}


