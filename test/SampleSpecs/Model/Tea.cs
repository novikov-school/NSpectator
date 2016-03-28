namespace SampleSpecs.Model
{
    class Tea
    {
        private readonly int temperature;

        public Tea(int temperature)
        {
            this.temperature = temperature;
        }

        public double Temperature { get; set; }

        public string Taste()
        {
            return temperature >= 210 ? "hot" : "cold";
        }
    }
}