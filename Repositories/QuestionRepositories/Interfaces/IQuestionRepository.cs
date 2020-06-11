using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Repositories.QuestionRepositories.Interfaces
{
    public interface IQuestionRepository
    {
        List<Regex> HelloPhrases { get; }
        List<Regex> MyNamePhrases { get; }
        List<Regex> JokePhrases { get; }
        List<Regex> DatePhrases { get; }
        List<Regex> ByePhrases { get; }
    }
}