using NUnit.Framework;
using TAlex.Mvvm.ViewModels;


namespace TAlex.Mvvm.Tests.ViewModels
{
    [TestFixture]
    public class ViewModelBaseTests
    {
        protected TestViewModel Target;


        [SetUp]
        public void SetUp()
        {
            Target = new TestViewModel();
        }


        #region PropertyChanged

        [Test]
        public void PropertyChanged_SetIntegerProperty_RaisePropertyChanged()
        {
            //arrange
            const int value = 3;
            string actualPropName = null;
            Target.PropertyChanged += (e, a) => { actualPropName = a.PropertyName; }; 

            //action
            Target.IntegerProperty = value;

            //assert
            Assert.AreEqual(value, Target.IntegerProperty);
            Assert.AreEqual("IntegerProperty", actualPropName);
        }

        [Test]
        public void PropertyChanged_SetStringProperty_RaisePropertyChanged()
        {
            //arrange
            const string value = "hello world";
            string actualPropName = null;
            Target.PropertyChanged += (e, a) => { actualPropName = a.PropertyName; };

            //action
            Target.StringProperty = value;

            //assert
            Assert.AreEqual(value, Target.StringProperty);
            Assert.AreEqual("StringProperty", actualPropName);
        }

        [Test]
        public void PropertyChanged_SetTheSameIntegerPropertyValue_DoNotRaisePropertyChanged()
        {
            //arrange
            const int value = 3;
            string actualPropName = null;
            Target.IntegerProperty = value;
            Target.PropertyChanged += (e, a) => { actualPropName = a.PropertyName; };

            //action
            Target.IntegerProperty = value;

            //assert
            Assert.AreEqual(value, Target.IntegerProperty);
            Assert.AreEqual(null, actualPropName);
        }

        #endregion


        #region Design Data

        public class TestViewModel : ViewModelBase
        {
            private int _intProp;
            private string _strProp;


            public int IntegerProperty
            {
                get { return _intProp; }
                set { Set(ref _intProp, value); }
            }

            public string StringProperty
            {
                get { return _strProp; }
                set { Set(() => StringProperty, ref _strProp, value); }
            }
        }

        #endregion
    }
}
