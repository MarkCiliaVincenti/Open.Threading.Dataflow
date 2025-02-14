﻿using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Open.Threading.Dataflow;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1047:Non-asynchronous method name should not end with 'Async'.", Justification = "To avoid ambiguity.")]
public static class TransformBlock
{
	public static TransformBlock<TIn, TOut> New<TIn, TOut>(Func<TIn, TOut> pipe, ExecutionDataflowBlockOptions? options = null)
		=> options is null
		? new TransformBlock<TIn, TOut>(pipe)
		: new TransformBlock<TIn, TOut>(pipe, options);

	public static TransformBlock<TIn, TOut> NewAsync<TIn, TOut>(Func<TIn, Task<TOut>> pipe, ExecutionDataflowBlockOptions? options = null)
		=> options is null
		? new TransformBlock<TIn, TOut>(pipe)
		: new TransformBlock<TIn, TOut>(pipe, options);
}
