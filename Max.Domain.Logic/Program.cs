using Max.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Max.Domain.Logic
{
    internal class Logic
    {
        static void Main(string[] args)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            SimpleTaskPlanner taskPlanner = new SimpleTaskPlanner();

            while (true)
            {
                WorkItem workItem = new WorkItem();

                Console.WriteLine("Enter the title:");
                workItem.title = Console.ReadLine();

                DateTime dueDate;
                while (true)
                {
                    Console.WriteLine("Enter the date in the format dd.MM.yyyy:");
                    string dateInput = Console.ReadLine();

                    if (DateTime.TryParseExact(dateInput, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out dueDate))
                    {
                        workItem.dueDate = dueDate;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Please enter the date in the format dd.MM.yyyy.");
                    }
                }

                Console.WriteLine("Select priority (none, low, medium, high, urgent):");
                if (Enum.TryParse<Priority>(Console.ReadLine(), true, out Priority priority))
                {
                    workItem.priority = priority;
                }
                else
                {
                    Console.WriteLine("Invalid priority. Setting default priority to 'none'.");
                    workItem.priority = Priority.none;
                }

                workItems.Add(workItem);

                Console.WriteLine("Enter + to add another item or - to proceed to sorting:");
                string input = Console.ReadLine();

                if (input == "-")
                {
                    break;
                }
            }

            Console.WriteLine("Enter the number of the sorting method. 1. By priority, 2. By title, 3. By date:");
            if (int.TryParse(Console.ReadLine(), out int sortMethod))
            {
                WorkItem[] sortedItems;
                switch (sortMethod)
                {
                    case 1:
                        sortedItems = taskPlanner.CreatePlan(workItems.ToArray(), taskPlanner.SortByPriority);
                        break;
                    case 2:
                        sortedItems = taskPlanner.CreatePlan(workItems.ToArray(), taskPlanner.SortByTitle);
                        break;
                    case 3:
                        sortedItems = taskPlanner.CreatePlan(workItems.ToArray(), taskPlanner.SortByDate);
                        break;
                    default:
                        Console.WriteLine("Invalid choice, sorting not performed.");
                        return;
                }

                Console.WriteLine("Sorting results:");
                foreach (var item in sortedItems)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Exiting.");
            }
        }
    }

    public class SimpleTaskPlanner
    {
        public WorkItem[] CreatePlan(WorkItem[] items, Comparison<WorkItem> comparison)
        {
            List<WorkItem> workItems = items.ToList();
            workItems.Sort(comparison);
            return workItems.ToArray();
        }

        public int SortByPriority(WorkItem firstItem, WorkItem secondItem)
        {
            return firstItem.priority.CompareTo(secondItem.priority);
        }

        public int SortByTitle(WorkItem firstItem, WorkItem secondItem)
        {
            return string.Compare(firstItem.title, secondItem.title, StringComparison.OrdinalIgnoreCase);
        }

        public int SortByDate(WorkItem firstItem, WorkItem secondItem)
        {
            return firstItem.dueDate.CompareTo(secondItem.dueDate);
        }
    }
}
