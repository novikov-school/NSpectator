﻿using NSpectator;

namespace SampleSpecs.Bug
{
    public class describe_after : nspec
    {
        string sequence = "";

        void before_each()
        {
            sequence += "1";
        }

        void it_is_one()
        {
            sequence.Is("1");
        }

        void it_is_still_just_one()
        {
            sequence.Is("1");
        }

        void after_each()
        {
            sequence = "";
        }
    }
}
