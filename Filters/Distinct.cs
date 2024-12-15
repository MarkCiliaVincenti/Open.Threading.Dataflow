using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace Open.Threading.Dataflow;

internal class DistinctFilter<T>(
	ITargetBlock<T> target,
	DataflowMessageStatus defaultResponseForDuplicate)
	: TargetBlockFilter<T>(target, defaultResponseForDuplicate, null)
{
    private readonly HashSet<T> _set = [];
	private readonly LockBackport _lock = LockFactory.Create();

    protected override bool Accept(T messageValue)
	{
		bool didntHave;
		lock (_lock) // Assure order of acceptance.
			didntHave = _set.Add(messageValue);
		return didntHave;
	}
}

public static partial class DataFlowExtensions
{
	public static ITargetBlock<T> Distinct<T>(this ITargetBlock<T> target, DataflowMessageStatus defaultResponseForDuplicate) => new DistinctFilter<T>(target, defaultResponseForDuplicate);
}
