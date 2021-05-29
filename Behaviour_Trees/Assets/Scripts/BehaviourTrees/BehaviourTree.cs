using System.Collections.Generic;

namespace BehaviourTrees
{
    public class BehaviourTree
    {
        private NodeEvaluationResult m_currentResult;

        private Node[] m_tree;


        public BehaviourTree()
        {
            m_currentResult = new NodeEvaluationResult
            {
                nextNodeIndex = 0,
                status = Status.START
            };
        }

        public void Tick()
        {
            do
            {
                m_currentResult = m_tree[m_currentResult.nextNodeIndex].Evaluate(m_currentResult.status);
            } while (m_currentResult.status != Status.WORKING);
        }
    }
}