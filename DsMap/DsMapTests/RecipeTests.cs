using System;
using System.Runtime.Remoting;
using System.Security.Cryptography.X509Certificates;
using DsMap;
using NUnit.Framework;

namespace DsMapTests
{
    [TestFixture]
    public class RecipeTests
    {
        [Test]
        public void ShouldPass()
        {
            Assert.That(true);
        }

        [Test]
        public void CanCreateCookbook()
        {
            Cookbook<Recipe> cookbook = DsMapper.FromRecipe<Recipe>();
            Assert.IsNotNull(cookbook);
        }

        [Test]
        public void ShouldBeAbleToCreateObject()
        {
            var example = new ThisClass();
            OtherClass created = DsMapper.FromRecipe<Recipe>().Create<OtherClass>(example);
            Assert.IsNotNull(created);
        }

        [Test]
        public void ShouldBeAbleToPassAValueThatIsInTheRecipe()
        {
            var example = new ThisClass();
            var randomValue = new Random((int) DateTime.Now.TimeOfDay.Ticks).Next();
            example.AValue = randomValue;

            OtherClass created = DsMapper.FromRecipe<Recipe>().Create<OtherClass>(example);

            Assert.IsNotNull(created);
            Assert.AreEqual(randomValue, example.AValue);
            Assert.AreEqual(randomValue, created.AValue);
        }
    }

    public class ThisClass
    {
        public int AValue;
    }

    public class OtherClass
    {
        public int AValue;
    }

    public class Recipe
    {
        public int AValue;
    }
}
