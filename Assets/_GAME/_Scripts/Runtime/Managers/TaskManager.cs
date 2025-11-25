using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Runtime
{
    public class TaskManager : Singleton<TaskManager>
    {
        public bool IsAllTaskCompleted { get; private set; } 
        public TaskBase CurrentTask { get; private set; }
        
        [SerializeField] private List<TaskBase> tasks = new();
        
        private int _currentTaskIndex = 0;

        public void Initialize()
        {
            CurrentTask = tasks[_currentTaskIndex];
            CurrentTask.ActivateTask();
        }
        
        public void CompleteTask(TaskBase task)
        {
            if (CurrentTask != task) return;
            
            CurrentTask?.DeactivateTask();
            _currentTaskIndex++;

            if (_currentTaskIndex >= tasks.Count)
            {
                IsAllTaskCompleted = true;
                CurrentTask = null;
                return;
            }
            
            CurrentTask = tasks[_currentTaskIndex];
            CurrentTask.ActivateTask();
        }

        public void InsertTask(TaskBase task)
        {
            CurrentTask?.DeactivateTask();
            tasks.Insert(_currentTaskIndex, task);
            CurrentTask = tasks[_currentTaskIndex];
            CurrentTask.ActivateTask();
        }
    }
}
