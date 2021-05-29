namespace BehaviourTrees
{
    public static class BehaviourTreeHelper
    {
        public static object GetParameter(NodeParameter parameter)
        {
            switch (parameter.type)
            {
                case ParameterType.CONST:
                    return parameter.data;
                case ParameterType.VARIABLE:
                    return null;
                default:
                    return null;
            }
        }
    }
}