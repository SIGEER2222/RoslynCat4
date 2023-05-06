using System.Collections.Immutable;
using System.Runtime.ExceptionServices;

namespace RoslynCat.Helpers
{
	/// <summary>
	/// ����İ����࣬�����С���
	/// </summary>
	[DebuggerNonUserCode]
	public static class Helpers
	{
		/// <summary>
		/// �������ָ������
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="method"></param>
		/// <param name="target"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static T Invoke<T>(this MethodBase method,object target,params object[] parameters) {
			try {
				return (T)method.Invoke(target,parameters);
			}
			catch (TargetInvocationException ex) {
				ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				return default(T);
			}
		}

		/// <summary>
		/// ��¼������ִ��ʱ��
		/// </summary>
		/// <param name="action">Fun<Task>���͵�ί�У�async () =>{}</Task></param>
		public static async void RecordExecutionTimeAsync(Func<Task> asyncAction) {
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			await asyncAction.Invoke();
			stopwatch.Stop();
			Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");
		}

		/// <summary>
		/// ת�����ɱ�����
		/// </summary>
		/// <typeparam name="TIn"></typeparam>
		/// <typeparam name="TOut"></typeparam>
		/// <param name="array"></param>
		/// <param name="mapper"></param>
		/// <returns></returns>
		public static ImmutableArray<TOut> SelectAsArray<TIn, TOut>(this ImmutableArray<TIn> array,Func<TIn,TOut> mapper) {
			if (array.IsDefaultOrEmpty) {
				return ImmutableArray<TOut>.Empty;
			}
			var builder = ImmutableArray.CreateBuilder<TOut>(array.Length);
			foreach (var e in array) {
				builder.Add(mapper(e));
			}
			return builder.MoveToImmutable();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static ImmutableArray<T> ToImmutableAndClear<T>(this ImmutableArray<T>.Builder builder) {
			if (builder.Capacity == builder.Count) {
				return builder.MoveToImmutable();
			}
			else {
				var result = builder.ToImmutable();
				builder.Clear();
				return result;
			}
		}
	}
}