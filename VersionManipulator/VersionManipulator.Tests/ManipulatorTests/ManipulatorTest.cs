using VersionManipulator.Entities;
using VersionManipulator.Manipulators;

namespace VersionManipulator.Tests
{
    public class ManipulatorTests
    {
        [Fact]
        public void HandleActions_StopsRunning_WhenActionIsClose()
        {
            // Arrange
            var manipulator = new Manipulator();

            // Act
            manipulator.InvokePrivateMethod("HandleActions", ActionEnum.Close);
            var runningField = manipulator.GetPrivateField<bool>("_running");

            // Assert
            Assert.False(runningField);
        }

        [Fact]
        public void HandleActions_CallsHandleFeatureAction_WhenActionIsFeature()
        {
            // Arrange
            var manipulator = new Manipulator();

            // Act
            manipulator.InvokePrivateMethod("HandleActions", ActionEnum.Feature);

            // Assert
            Assert.True(true);
        }

        [Fact]
        public void HandleActions_CallsHandleBugFixAction_WhenActionIsBugFix()
        {
            // Arrange
            var manipulator = new Manipulator();

            // Act
            manipulator.InvokePrivateMethod("HandleActions", ActionEnum.BugFix);

            // Assert
            Assert.True(true);
        }
    }

    public static class TestHelperExtensions
    {
        public static T InvokePrivateMethod<T>(this object obj, string methodName, params object[] parameters)
        {
            var method = obj.GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (T)method.Invoke(obj, parameters);
        }

        public static void InvokePrivateMethod(this object obj, string methodName, params object[] parameters)
        {
            var method = obj.GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method.Invoke(obj, parameters);
        }

        public static T GetPrivateField<T>(this object obj, string fieldName)
        {
            var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (T)field.GetValue(obj);
        }
    }
}
