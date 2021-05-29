
using System;
using UnityEngine;

namespace BehaviourTrees
{
    public struct NodeEvaluationResult
    {
        public Status status;
        public int nextNodeIndex;
    }
    
    public abstract class Node
    {
        public int ParentIndex;
        public NodeParameter[] parameters;

        public abstract NodeEvaluationResult Evaluate(Status receivedStatus);
    }

    public abstract class ActionNode : Node
    {
    }

    public abstract class CompositeNode : Node
    {
        public int[] ChildIndices;
        
    }

    public abstract class DecoratorNode : Node
    {
        public int ChildIndex;
    }

    public class InverterNode : DecoratorNode
    {
        public override NodeEvaluationResult Evaluate(Status receivedStatus)
        {
            NodeEvaluationResult result = new NodeEvaluationResult();

            switch (receivedStatus)
            {
                case Status.START:
                case Status.TRAVERSE:
                case Status.WORKING:
                    result.nextNodeIndex = ChildIndex;
                    result.status = Status.TRAVERSE;
                    break;
                
                case Status.SUCCESS:
                case Status.FAILED:

                    result.status = (receivedStatus == Status.SUCCESS)? Status.FAILED : Status.SUCCESS;
                    result.nextNodeIndex = ParentIndex;
                    break;
            }

            return result;
        }
    }

    public class SelectorNode : CompositeNode
    {
        public override NodeEvaluationResult Evaluate(Status receivedStatus)
        {
            NodeEvaluationResult result = new NodeEvaluationResult
            {
                nextNodeIndex = (int) BehaviourTreeHelper.GetParameter(parameters[0]),
                status = Status.TRAVERSE
            };
            
            switch (receivedStatus)
            {
                case Status.START:
                case Status.TRAVERSE:
                case Status.WORKING:
                    result.nextNodeIndex = (int) BehaviourTreeHelper.GetParameter(parameters[0]);
                    result.status = Status.TRAVERSE;
                    break;
                
                case Status.SUCCESS:
                case Status.FAILED:

                    result.status = receivedStatus;
                    result.nextNodeIndex = ParentIndex;
                    break;
            }

            return result;
        }
    }

    public class LogActionNode : ActionNode
    {
        public override NodeEvaluationResult Evaluate(Status receivedStatus)
        {
            Debug.Log(BehaviourTreeHelper.GetParameter(parameters[0]));

            return new NodeEvaluationResult
            {
                nextNodeIndex = ParentIndex,
                status = Status.SUCCESS
            };
        }
    }
}