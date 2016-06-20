#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System.Collections.Generic;
using NSpectator;
using Slant.Expectations;

namespace SampleSpecs.Bug
{
    public class Grandparents_run_first : Spec
    {
        List<int> ints = null;

        void describe_as_rspec() // describe RSpec do
        {
            Before = () => ints = new List<int>(); //  before(:each) { @array = Array.new }

            Context["something that works in rspec but not with NSpectator"] = () => //  context "something that works in rspec but not nspec" do
            {
                Before = () => ints.Add(1);

                Describe["sibling context"] = () => //    context "sibling context" do
                {
                    Before = () => ints.Add(1); //      before(:each) { @array << "sibling 1" }

                    Specify = () => ints.Count.Expected().ToBe(1); //      it { @array.count.should == 1 }
                }; //    end

                Describe["another sibling context"] = () => //    context "another sibling context" do
                {
                    Before = () => ints.Add(1); //      before(:each) { @array << "sibling 2" }

                    Specify = () => ints.Count.Expected().ToBe(1); //      it { @array.count.should == 1 }
                }; //    end
            }; //  end
        } //end
    }
}