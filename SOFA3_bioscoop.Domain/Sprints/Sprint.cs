using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using SOFA_bioscoop.Domain.Pipelines;

namespace SOFA_bioscoop.Domain
{
    public class Sprint
    {
        private string name;
        private DateTime startDate;
        private DateTime endDate;
        private List<BacklogItem> sprintBacklog = new List<BacklogItem>();
        private ISprintTypeStrategy strategy;
        private Project linkedProject;
        private ISprintState state;
        private string? reviewSummary;
        public DevelopmentPipeline? developmentPipeline;
        public INotificationService? notificationService;
        private List<Person> scrumTeam = new List<Person>();

        // Alle states als attributes
        private readonly ISprintState createdState;
        private readonly ISprintState inProgressState;
        private readonly ISprintState finishedState;
        private readonly ISprintState releasingState;
        private readonly ISprintState releasedState;
        private readonly ISprintState failedReleaseState;
        private readonly ISprintState cancelledState;
        private readonly ISprintState inReviewState;
        private readonly ISprintState reviewedState;
        private readonly ISprintState closedState;

        public Sprint(string name,
                      DateTime startDate,
                      DateTime endDate,
                      List<BacklogItem> sprintBacklog,
                      ISprintTypeStrategy strategy,
                      Project linkedProject,
                      DevelopmentPipeline? developmentPipeline = null,
                      INotificationService? notificationService = null)
        {
            this.name = name;
            this.startDate = startDate;
            this.endDate = endDate;
            this.sprintBacklog = sprintBacklog;
            this.strategy = strategy;
            this.linkedProject = linkedProject;
            this.developmentPipeline = developmentPipeline;
            this.notificationService = notificationService;

            // States initialiseren
            createdState = new CreatedState();
            inProgressState = new InProgressState();
            finishedState = new FinishedState();
            releasingState = new ReleasingState();
            releasedState = new ReleasedState();
            failedReleaseState = new FailedReleaseState();
            cancelledState = new CancelledState();
            inReviewState = new InReviewState();
            reviewedState = new ReviewedState();
            closedState = new ClosedState();

            // Beginstate
            this.state = createdState;
        }


        public ISprintState GetCreatedState() => createdState;
        public ISprintState GetInProgressState() => inProgressState;
        public ISprintState GetFinishedState() => finishedState;
        public ISprintState GetReleasingState() => releasingState;
        public ISprintState GetReleasedState() => releasedState;
        public ISprintState GetFailedReleaseState() => failedReleaseState;
        public ISprintState GetCancelledState() => cancelledState;
        public ISprintState GetInReviewState() => inReviewState;
        public ISprintState GetReviewedState() => reviewedState;
        public ISprintState GetClosedState() => closedState;

        /// <summary>Compatibiliteit met oudere code / tests (zelfde als NotificationService property).</summary>
        public INotificationService? NotificationService
        {
            get => notificationService;
            set => notificationService = value;
        }

        public void AddPerson(Person person) => AddTeamMember(person);

        public Person GetScrumMaster()
        {
            foreach (Person person in scrumTeam)
            {
                if (person.Role == Role.ScrumMaster)
                    return person;
            }

            throw new InvalidOperationException("No Scrum Master in sprint team.");
        }

        public Person GetProductOwner()
        {
            foreach (Person person in scrumTeam)
            {
                if (person.Role == Role.ProductOwner)
                    return person;
            }

            throw new InvalidOperationException("No Product Owner in sprint team.");
        }
        public ISprintState GetPostFinishedState() => strategy.getPostFinishState(this);

        public void SetState(ISprintState state) => this.state = state;

        public void SetProject(Project project)
        {
            linkedProject = project;
        }
        public void SetReviewSummary(string summary)
        {
            reviewSummary = summary;
        }

        public void AddTeamMember(Person person)
        {
            scrumTeam.Add(person);
        }
        public void RemoveTeamMember(Person person)
        {
            scrumTeam.Remove(person);
        }
        public bool HasReviewSummary()
        {
            return reviewSummary != null;
        }

        public void UploadReviewSummary(string summary)
        {
            state.UploadReviewSummary(this, summary);
        }
        

        public void MarkAsReviewed()
        {
            state.MarkAsReviewed(this);
        }

        // Sprint methodes delegeren naar state
        public void EditName(string name)
        {
            state.ValidateEdit(this);
            this.name = name;
        }

        public void EditStartDate(DateTime startDate)
        {
            state.ValidateEdit(this);
            this.startDate = startDate;
        }

        public void EditEndDate(DateTime endDate)
        {
            state.ValidateEdit(this);
            this.endDate = endDate;
        }

        public void AddBacklogItem(BacklogItem item)
        {
            state.AddBacklogItem(this, item);
        }

        public void StartSprint()
        {
            state.StartSprint(this);
        }

        public void FinishSprint()
        {
            state.FinishSprint(this);
        }

        public void HandlePostFinish()
        {
            state.HandlePostFinish(this);
        }

        public DevelopmentPipeline? Pipeline
        {
            get { return developmentPipeline; }
            set { developmentPipeline = value; }
        }

        public void RunReleasePipeline()
        {
            try
            {
                developmentPipeline?.ReleasePipeline();
            }
            catch (Exception)
            {
                if (notificationService != null && developmentPipeline != null)
                {
                    Person sm = GetScrumMaster();
                    notificationService.Send(sm, "Pipeline failed");
                }
            }
        }

        public void StartPipeline()
        {
            state.StartPipeline(this);
        }

        public void OnPipelineSuccess()
        {
            state.OnPipelineSuccess(this);
        }

        public void OnPipelineFailure()
        {
            state.OnPipelineFailure(this);
        }

        public void RetryRelease()
        {
            state.RetryRelease(this);
        }

        public void CancelRelease()
        {
            state.CancelRelease(this);
        }

        public void ExecutePostFinish()
        {
            strategy.ExecutePostFinish(this);
        }

    }
}


