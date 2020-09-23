using Xamarin.Forms.Internals;

namespace Hinnova.Behaviors
{
    [Preserve(AllMembers = true)]
    public interface IAction
    {
        bool Execute(object sender, object parameter);
    }
}