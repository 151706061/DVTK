// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;
using System.Windows.Forms;
using DvtTreeNodeTag;

namespace Dvt
{
	/// <summary>
	/// Class containing extra information on the nodes in the Session Tree.
	/// </summary>
	public class NodesInformation
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public NodesInformation()
		{			
			// Empty.
		}

		/// <summary>
		/// Store information on which nodes (the supplied node and all sub nodes)
		/// are expanded.
		/// </summary>
		/// <param name="theTreeNode">The tree node.</param>
		public void StoreExpandInformation(TreeNode theTreeNode)
		{
			if (theTreeNode.IsExpanded)
			{
				_TagsOfExpandedNodes.Add(theTreeNode.Tag);
			}

			// Do the same for all sub nodes.
			foreach(TreeNode subTreeNode in theTreeNode.Nodes)
			{
				StoreExpandInformation(subTreeNode);	
			}
		}

		/// <summary>
		/// Expand the supplied node and sub nodes back if they were expanded before.
		/// </summary>
		/// <param name="theTreeNode">The tree node.</param>
		public void RestoreExpandInformation(TreeNode theTreeNode)
		{
			TreeNodeTag theTreeNodeTagForCurrentNode = (TreeNodeTag)theTreeNode.Tag;
			TreeNodeTag theFoundTreeNodeTag = null;

			// Find out if the node was previously expanded.
			foreach(TreeNodeTag theTreeNodeTag in _TagsOfExpandedNodes)
			{
				if (theTreeNodeTagForCurrentNode.IsRepresentingSame(theTreeNodeTag))
				{
					theFoundTreeNodeTag = theTreeNodeTag;
					break;
				}
			}

			// If this node was expanded, expand it back.
			if (theFoundTreeNodeTag != null)
			{
				theTreeNode.Expand();
				_TagsOfExpandedNodes.Remove(theFoundTreeNodeTag);
			}

			// Do the same for all sub nodes.
			foreach(TreeNode subTreeNode in theTreeNode.Nodes)
			{
				RestoreExpandInformation(subTreeNode);	
			}
		}

		/// <summary>
		/// Remove all expanded information for the supplied session.
		/// </summary>
		/// <param name="theSession">The session.</param>
		public void RemoveExpandInformationForSession(Dvtk.Sessions.Session theSession)
		{
			ArrayList theTagsToRemove = new ArrayList();

			foreach(TreeNodeTag theTreeNodeTag in _TagsOfExpandedNodes)
			{
				if (theTreeNodeTag._Session == theSession)
				{
					theTagsToRemove.Add(theTreeNodeTag);
				}
			}

			foreach(TreeNodeTag theTreeNodeTagToRemove in theTagsToRemove)
			{
				_TagsOfExpandedNodes.Remove(theTreeNodeTagToRemove);
			}
		}

		/// <summary>
		/// Remove all expanded information.
		/// </summary>
		public void RemoveAllExpandInformation()
		{
			_TagsOfExpandedNodes.Clear();
		}

		public void StoreSelectedNode(TreeView theTreeView)
		{
			TreeNode theSelectedTreeNode = theTreeView.SelectedNode;

			if (theSelectedTreeNode != null)
			{
				_TagOfSelectedNode = (TreeNodeTag)theSelectedTreeNode.Tag;
			}
			else
			{
				_TagOfSelectedNode = null;
			}
		}

		/// <summary>
		/// If nodes exist:
		/// 1. Try to select the node with the same representing tag.
		/// 2. If this fails, find the session node with the same session as the tag.
		/// 3. If this fails, select the first session node.
		/// </summary>
		/// <param name="theTreeView"></param>
		public void RestoreSelectedNode(TreeView theTreeView)
		{
			TreeNode theTreeNodeToSelect = null;

			if (theTreeView.Nodes.Count > 0)
			{
				// 1. Try to select the node with the same representing tag.
				foreach(TreeNode theSessionNode in theTreeView.Nodes)
				{
					theTreeNodeToSelect = FindTreeNode(theSessionNode, _TagOfSelectedNode);

					if (theTreeNodeToSelect != null)
					{
						break;
					}
				}

				// 2.If this fails, find the session node with the same session as the tag.
				if (theTreeNodeToSelect == null)
				{
					foreach(TreeNode theSessionNode in theTreeView.Nodes)
					{
						TreeNodeTag theSessionTreeNodeTag = (TreeNodeTag)theSessionNode.Tag;

						if (theSessionTreeNodeTag.IsRepresentingSame(_TagOfSelectedNode))
						{
							theTreeNodeToSelect = theSessionNode;
							break;
						}
					}				
				}

				// 3. If this fails, select the first session node.
				if (theTreeNodeToSelect == null)
				{
					theTreeNodeToSelect = theTreeView.Nodes[0];
				}

				theTreeView.SelectedNode = theTreeNodeToSelect;
			}

			_TagOfSelectedNode = null;
		}

		private TreeNode FindTreeNode(TreeNode theTreeNode, TreeNodeTag theRepresentingTreeNodeTag)
		{
			TreeNode theTreeNodeToFind = null;

			TreeNodeTag theTreeNodeTag = (TreeNodeTag)theTreeNode.Tag;

			if (theTreeNodeTag.IsRepresentingSame(theRepresentingTreeNodeTag))
			{
				theTreeNodeToFind = theTreeNode;
			}
			else
			{
				foreach(TreeNode subTreeNode in theTreeNode.Nodes)
				{
					theTreeNodeToFind = FindTreeNode(subTreeNode, theRepresentingTreeNodeTag);
					
					if (theTreeNodeToFind != null)
					{
						break;
					}
				}
			}

			return(theTreeNodeToFind);
		}

		ArrayList _TagsOfExpandedNodes = new ArrayList();
		TreeNodeTag _TagOfSelectedNode = null;
	}
}
