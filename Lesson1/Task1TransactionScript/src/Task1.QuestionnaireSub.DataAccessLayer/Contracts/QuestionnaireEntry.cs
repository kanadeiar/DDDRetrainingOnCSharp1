namespace Task1.QuestionnaireSub.DataAccessLayer.Contracts;

public class QuestionnaireEntry
{
    public int Id { get; set; }
    
    public string? SurName { get; set; }
    
    public string? Name { get; set; }
    
    public int Age { get; set; }
    
    public int Height { get; set; }
    
    public int Weight { get; set; }
}