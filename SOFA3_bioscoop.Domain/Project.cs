using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SOFA_bioscoop.Domain
{
    public class Project
    {
        public string name;
        private List<Sprint> sprints = new List<Sprint>();

        public void AddSprint(Sprint sprint)
        {
            if (!sprints.Contains(sprint))
            {
                sprints.Add(sprint);
                sprint.SetProject(this);
            }
        }
    }
}



