namespace TaskManagerMVC.Models.Enums;

public enum HistoryActionType
{
    Created = 0,
    TitleChanged = 1,
    DescriptionChanged = 2,
    StatusChanged = 3,
    PriorityChanged = 4,
    ProjectChanged = 5,
    AssigneeChanged = 6,
    DueDateChanged = 7,
    EstimatedHoursChanged = 8,
    Deleted = 9
}
