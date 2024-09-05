using System;

namespace Max.Domain.Models
{
    public enum Priority
    {
        none = 0,
        low = 1,
        medium = 2,
        high = 3,
        urgent = 4,
    }
    public enum Complexily
    {
        none = 0,
        minutes = 1,
        hours = 2,
        days = 3,
        weeks = 4,
    }

    public class WorkItem
    {
        public DateTime creationDate;
        public DateTime dueDate;
        public Priority priority;
        public Complexily complexily;
        public string title;
        public string description;
        public bool isCompleted;
        public override string ToString()
        {
            return $"{title}: due to {dueDate:dd.MM.yyyy}, " +
                   $"{priority.ToString().ToLower()} priority, " +
                   $"complexity: {complexily.ToString().ToLower()}";
        }
    }
    internal class Model
    {
        static void Main(string[] args)
        {

        }
    }
}
