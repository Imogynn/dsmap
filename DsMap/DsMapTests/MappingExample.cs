using System;
using System.Collections.Generic;
using System.Linq;
using DsMap;
using NUnit.Framework;

namespace DsMapTests
{
    [TestFixture]
    public class MappingExample
    {
        [Test]
        public void Example()
        {
            var aThing = new FirstClass();
            var randomNumber = new Random((int) DateTime.Now.TimeOfDay.Ticks).Next();
            aThing.Value = randomNumber;

            // this is beautiful
            var mappedThing = (SecondClass) (FirstClassToSecondClass) aThing;
            Assert.IsNotNull(mappedThing);
            
        }

        [Test]
        public void ContainerClassExample()
        {
            var aBunchOfThings = new FirstClassGroup();
            var randomNumber = new Random((int)DateTime.Now.TimeOfDay.Ticks).Next();
            aBunchOfThings.Things.Add(new FirstClass {Value = randomNumber});

            // note this is an example and not really a test as I haven't yet decided that we should
            // move lists into dsmap proper... this seems more likely to be happy longer term
            var mappedBunchOfThings = (SecondClassGroup)(FirstClassGroupToSecondClassGroup) aBunchOfThings;
            Assert.AreEqual(1, mappedBunchOfThings.Things.Count);
            Assert.AreEqual(randomNumber, mappedBunchOfThings.Things.First().Value);
        }
    }

    public class FirstClassGroupToSecondClassGroup
    {
        public List<FirstClassToSecondClass> Things;
        public static explicit operator FirstClassGroupToSecondClassGroup(FirstClassGroup given)
        {
            var result = new FirstClassGroupToSecondClassGroup();
            result.Things = given.Things.Select(gvn => (FirstClassToSecondClass) gvn).ToList();
            return result;
        }
        public static explicit operator SecondClassGroup(FirstClassGroupToSecondClassGroup given)
        {
            var result = new SecondClassGroup();
            result.Things = given.Things.Select(gvn => (SecondClass) gvn).ToList();
            return result;
        }
    }

    public class SecondClassGroup
    {
        public List<SecondClass> Things;
    }

    public class FirstClassGroup
    {
        public List<FirstClass> Things;
        public FirstClassGroup()
        {
            Things = new List<FirstClass>();
        }
    }

    public sealed class FirstClass
    {
        public int Value;
    }

    public sealed class SecondClass
    {
        public int Value;
    }

    public class FirstClassToSecondClass
    {
        public int Value;

        public static explicit operator FirstClassToSecondClass(FirstClass given)
        {
            // no boring code here
            return DsMapper.FromRecipe<FirstClassToSecondClass>().Create<FirstClassToSecondClass>(given);
        }

        public static explicit operator SecondClass(FirstClassToSecondClass given)
        {
            // no boring code here either
            return DsMapper.FromRecipe<SecondClass>().Create<SecondClass>(given);
        }
    }


}
