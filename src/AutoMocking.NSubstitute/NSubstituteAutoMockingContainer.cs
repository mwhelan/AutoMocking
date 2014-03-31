namespace AutoMocking.NSubstitute
{
    public class NSubstituteAutoMockingContainer : AutoMockingContainer
    {
        public NSubstituteAutoMockingContainer() : base(new NSubstituteDependencyFactory())
        {
        }
    }
}