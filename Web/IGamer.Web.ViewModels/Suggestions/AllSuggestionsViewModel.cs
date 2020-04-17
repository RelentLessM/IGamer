namespace IGamer.Web.ViewModels.Suggestions
{
    using System.Collections.Generic;

    public class AllSuggestionsViewModel
    {
        public IEnumerable<SuggestionViewModel> Suggestions { get; set; }

        public IEnumerable<SuggestionForDropDownViewModel> SuggestionsForDropDown { get; set; }
    }
}
