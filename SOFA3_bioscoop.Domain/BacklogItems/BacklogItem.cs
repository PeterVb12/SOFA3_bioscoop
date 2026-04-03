using SOFA_bioscoop.Domain.BacklogItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA_bioscoop.Domain
{
    public class BacklogItem
    {
        public string Title { get; set; }
        public string Description { get; set; }

        private IBacklogitemState state;
        public List<Thread> threads = new List<Thread>();

        // Alle states als attributes
        private readonly IBacklogitemState todoState;
        private readonly IBacklogitemState doingState;
        private readonly IBacklogitemState readyForTestingState;
        private readonly IBacklogitemState testingState;
        private readonly IBacklogitemState testedState;
        private readonly IBacklogitemState doneState;

        // Notificatie services voor testers en scrum master
        private readonly List<INotificationService> notificationServices;
        private readonly List<Person> testers;
        private readonly Person scrumMaster;

        public BacklogItem(string title, string description, List<Person> testers, Person scrumMaster, List<INotificationService> notificationServices)
        {
            Title = title;
            Description = description;
            this.testers = testers;
            this.scrumMaster = scrumMaster;
            this.notificationServices = notificationServices;

            // States initialiseren
            todoState = new TodoState();
            doingState = new DoingState();
            readyForTestingState = new ReadyForTestingState();
            testingState = new TestingState();
            testedState = new TestedState();
            doneState = new DoneState();

            // Beginstate
            state = todoState;
        }

        // State getters
        public IBacklogitemState GetTodoState() => todoState;
        public IBacklogitemState GetDoingState() => doingState;
        public IBacklogitemState GetReadyForTestingState() => readyForTestingState;
        public IBacklogitemState GetTestingState() => testingState;
        public IBacklogitemState GetTestedState() => testedState;
        public IBacklogitemState GetDoneState() => doneState;

        public void SetState(IBacklogitemState state) => this.state = state;

        // Publieke methodes die delegeren naar state
        public void StartWork() => state.StartWork(this);
        public void ReadyForTesting() => state.ReadyForTesting(this);
        public void StartTesting() => state.StartTesting(this);
        public void FinishTesting() => state.FinishTesting(this);
        public void Approve() => state.Approve(this);
        public void Deny() => state.Deny(this);
        public void Reject() => state.Reject(this);

        // Thread beheer
        public void AddThread(Thread thread) => state.AddThread(this, thread);
        public IReadOnlyList<Thread> GetThreads() => threads.AsReadOnly();

        // Notificatie methodes
        public void NotifyTesters()
        {
            testers.ForEach(tester =>
                notificationServices.ForEach(service =>
                    service.Send(tester, $"Backlog item '{Title}' is klaar voor testing.")));
        }

        public void NotifyScrumMaster()
        {
            notificationServices.ForEach(service =>
                service.Send(scrumMaster, $"Backlog item '{Title}' is afgekeurd en teruggegaan naar Todo."));
        }
    }
}
