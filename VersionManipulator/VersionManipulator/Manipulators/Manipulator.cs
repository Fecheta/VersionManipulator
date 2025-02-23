using VersionManipulator.Constants;
using VersionManipulator.Entities;
using VersionManipulator.Extensions;

namespace VersionManipulator.Manipulators
{
    public class Manipulator
    {
        private readonly FileManipulator _fileManipulator;

        private bool _running = true;
        private readonly string _invalidMessage = "THE ACTION IS NOT VALID!";
        private readonly string _fileErrorMessage = "ERROR WHILE HANDELING THE FILE";

        public Manipulator()
        {
            _fileManipulator = new FileManipulator();
        }

        public void Start()
        {


            while (_running)
            {
                var action = ReadAction();
                HandleActions(action);
            }
        }

        private ActionEnum? ReadAction()
        {
            Console.WriteLine("Choose atction for version: 'Feature', 'BugFix' or 'Close' :");
            Console.Write("> ");

            var inputedAction = Console.ReadLine();


            if (inputedAction == null) { return null; }

            if (inputedAction.Equals(ActionEnum.Feature.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return ActionEnum.Feature;
            }

            if (inputedAction.Equals(ActionEnum.BugFix.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return ActionEnum.BugFix;
            }

            if (inputedAction.Equals(ActionEnum.Close.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return ActionEnum.Close;
            }

            return null;
        }

        private void HandleActions(ActionEnum? action)
        {
            switch (action)
            {
                case ActionEnum.Feature:
                    HandleFeatureAction();
                    break;
                case ActionEnum.BugFix:
                    HandleBugFixAction();
                    break;
                case ActionEnum.Close:
                    _running = false;
                    break;
                default:
                    Console.WriteLine(_invalidMessage);
                    break;
            }
        }

        private void HandleFeatureAction()
        {
            HandleActionGeneric(version => version.IncreaseBuild());
        }

        private void HandleBugFixAction()
        {
            HandleActionGeneric(version => version.IncreaseRevision());
        }

        private void HandleActionGeneric(Action<Entities.Version> action)
        {
            try
            {
                var versionText = _fileManipulator.ReadVersionFromFile(FilePaths.VersionFile);

                var version = versionText.ParseVersion();
                action(version);

                _fileManipulator.WriteVersionFromFile(FilePaths.VersionFile, version.GetString());
                Console.WriteLine($"> {versionText} => {version.GetString()}");
            }
            catch (Exception ex)
            {
                {
                    Console.WriteLine($"{_fileErrorMessage}: {ex.Message}");
                    _running = false;
                }
            }

        }
    }
}
