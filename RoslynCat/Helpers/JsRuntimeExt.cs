﻿using Microsoft.JSInterop;

namespace RoslynCat.Helpers
{

	public static class JsRuntimeExt
	{
		public static IJSRuntime? Shared { get; set; }

		public static async Task SafeInvokeAsync(this IJSRuntime jsRuntime,string identifier,params object[] args) {
			if (jsRuntime == null)
				return;
			await jsRuntime.InvokeVoidAsync(identifier,args);
		}

		public static async Task<T> SafeInvokeAsync<T>(this IJSRuntime jsRuntime,string identifier,params object[] args) {
			if (jsRuntime == null)
				return default!;
			return await jsRuntime.InvokeAsync<T>(identifier,args);
		}

		public static async Task CreateMonacoEditorAsync(this IJSRuntime js,string editorId,string code) {
			await js.SafeInvokeAsync("monacoInterop.createEditor",editorId,code);
		}

		public static async Task Test(this IJSRuntime js) {
			await js.SafeInvokeAsync("monacoInterop.Test");
		}

		public static async Task<string> GetValue(this IJSRuntime js,string editorId) {
			return await js.SafeInvokeAsync<string>("monacoInterop.getCode",editorId);
		}
		public static async Task SetValue(this IJSRuntime js,string editorId,string code) {
			await js.SafeInvokeAsync("monacoInterop.setCode",editorId,code);
		}
		public static async Task RegisterMonacoProvidersAsync(this IJSRuntime js,string editorId) {
			await js.SafeInvokeAsync("monacoInterop.change",editorId);
		}
		public static async Task CopyUrl(this IJSRuntime js,string text) {
			await js.SafeInvokeAsync("monacoInterop.copyText",text);
		}

		public static async Task<string> GetOldCode(this IJSRuntime js) {
			return await js.SafeInvokeAsync<string>("monacoInterop.getOldCode");
		}

		// public static async Task CreateMonacoEditorAsync(this IJSRuntime jsRuntime, string editorId, Dictionary<string, object> options)
		//{
		//    await jsRuntime.InvokeVoidAsync("CreateMonacoEditor", editorId, options);
		//}

		//public static async Task RegisterMonacoProvidersAsync(this IJSRuntime jsRuntime, string editorId, DotNetObjectReference<MonacoService> dotnetHelper)
		//{
		//    await jsRuntime.InvokeVoidAsync("RegisterMonacoProviders", editorId, dotnetHelper);
		//}

		//public static async Task SetMonacoValueAsync(this IJSRuntime jsRuntime, string editorId, string value)
		//{
		//    await jsRuntime.InvokeVoidAsync("SetMonacoValue", editorId, value);
		//}

		//public static ValueTask<string> GetMonacoValueAsync(this IJSRuntime jsRuntime, string editorId)
		//{
		//    return jsRuntime.InvokeAsync<string>("GetMonacoValue", editorId);
		//}

		//public static async Task SetMonacoDiagnosticsAsync(this IJSRuntime jsRuntime, string editorId, List<Diagnostic> diagnostics)
		//{
		//    await jsRuntime.InvokeVoidAsync("SetMonacoDiagnostics", editorId, diagnostics);
		//}
	}
}
