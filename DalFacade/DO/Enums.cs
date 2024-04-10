namespace DO;
/// <summary>
/// EngineerExperience= the level of the engineer or the level of the task
/// Entities= choosin entities for the main frogramme switch
/// Actions= choosing actions to perform on each entity
/// </summary>
public enum EngineerExperience { Beginner, AdvancedBeginner, Intermediate, Advanced, Expert };
public enum Entities { EXIT = 0, ENGINEER, TASK, DEPENDENCY };
public enum Actions { BACK_TO_MAIN = 0, CREATE, READ, READ_ALL, UPDATE, DELETE };

