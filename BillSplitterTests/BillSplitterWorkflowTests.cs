using BillSplitterConsole.Model;
using BillSplitterConsole.Workflow;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace BillSplitterTests
{
    public class BillSplitterWorkflowTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCaseSource(typeof(BillSplitterWorkflowTestData), "TestCases")]
        public void AllocateExpensesAndPaymentsTest(Queue<object> testData, int tripCount, List<ParticipantBalance> testResults)
        {
            var workflow = new BillSplitterWorkflow();
            List<Trip> trips = workflow.AllocateExpenses(testData);
            trips = BillSplitterWorkflow.AllocatePayments(trips);

            Assert.AreEqual(tripCount, trips.Count);

            if (trips.Count > 0)
            {
                List<ParticipantBalance> participantBalances = (List<ParticipantBalance>)trips[0].GetBalances();

                Assert.AreEqual(testResults.Count, participantBalances.Count);
                for (int i = 0; i < participantBalances.Count; i++)
                {
                    Assert.AreEqual(testResults[i].Balance, participantBalances[i].Balance);
                }
            }
        }
    }

    public class BillSplitterWorkflowTestData
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(GetTest1Data(), 1, GetTest1Result()).SetName("Typical Case");
                yield return new TestCaseData(GetTest0Data(), 0, GetTest0Result()).SetName("Empty Case");
            }
        }

        private static Queue<object> GetTest0Data()
        {
            Queue<object> queue = new Queue<object>();

            queue.Enqueue(0);

            return queue;
        }

        private static List<ParticipantBalance> GetTest0Result()
        {
            List<ParticipantBalance> participantBalances = new List<ParticipantBalance>();
            return participantBalances;
        }


        private static Queue<object> GetTest1Data()
        {
            Queue<object> queue = new Queue<object>();
            
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(1.00M);
            queue.Enqueue(3.00M);
            queue.Enqueue(5.00M);
            queue.Enqueue(2);
            queue.Enqueue(2.00M);
            queue.Enqueue(4.00M);
            queue.Enqueue(0);

            return queue;
        }

        private static List<ParticipantBalance> GetTest1Result()
        {
            List<ParticipantBalance> participantBalances = new List<ParticipantBalance>();
            
            ParticipantBalance participantBalance = new ParticipantBalance(1, -1.5M);
            participantBalances.Add(participantBalance);
            participantBalance = new ParticipantBalance(2, 1.5M);
            participantBalances.Add(participantBalance);

            return participantBalances;
        }
    }
}