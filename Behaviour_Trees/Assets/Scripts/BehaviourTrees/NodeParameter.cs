namespace BehaviourTrees
{
    public enum ParameterType
    {
        CONST,
        VARIABLE
    }
    
    public struct NodeParameter
    {
        public ParameterType type;
        public object data;
    }
}