﻿using Microsoft.Extensions.Logging;
using System;
using System.Text;
using Xunit.Abstractions;

namespace ToMqttNet.Test.Unit;

public class XUnitLogger<T> : XUnitLogger, ILogger<T>
{
	public XUnitLogger(ITestOutputHelper testOutputHelper, LoggerExternalScopeProvider scopeProvider)
		: base(testOutputHelper, scopeProvider, typeof(T).FullName!)
	{
	}
}

public class XUnitLoggerProvider : ILoggerProvider
{
	private readonly ITestOutputHelper _testOutputHelper;
	private readonly LoggerExternalScopeProvider _scopeProvider = new();

	public XUnitLoggerProvider(ITestOutputHelper testOutputHelper)
	{
		_testOutputHelper = testOutputHelper;
	}

	public ILogger CreateLogger(string categoryName)
	{
		return new XUnitLogger(_testOutputHelper, _scopeProvider, categoryName);
	}

	public void Dispose()
	{
	}
}

public static class TestOutputHelperExtensions
{
	public static ILoggerProvider CreateProvider(this ITestOutputHelper testOutput)
	{
		return new XUnitLoggerProvider(testOutput);
	}

	public static ILogger CreateLogger(this ITestOutputHelper testOutputHelper)
	{
		return new XUnitLogger(testOutputHelper, new LoggerExternalScopeProvider(), "");
	}

	public static ILogger<T> CreateLogger<T>(this ITestOutputHelper testOutputHelper)
	{
		return new XUnitLogger<T>(testOutputHelper, new LoggerExternalScopeProvider());
	}
}

public class XUnitLogger : ILogger
{
	private readonly ITestOutputHelper _testOutputHelper;
	private readonly string _categoryName;
	private readonly LoggerExternalScopeProvider _scopeProvider;

	public XUnitLogger(ITestOutputHelper testOutputHelper, LoggerExternalScopeProvider scopeProvider, string categoryName)
	{
		_testOutputHelper = testOutputHelper;
		_scopeProvider = scopeProvider;
		_categoryName = categoryName;
	}

	public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

	public IDisposable BeginScope<TState>(TState state) => _scopeProvider.Push(state);

	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
	{
		var sb = new StringBuilder()
			.Append('[')
			.Append(DateTimeOffset.Now.ToString())
			.Append(' ')
			.Append(GetLogLevelString(logLevel))
			.Append(' ')
			.Append(_categoryName)
			.Append("] ")
			.Append(formatter(state, exception));

		if (exception != null)
		{
			sb.Append('\n').Append(exception);
		}

		// Append scopes
		_scopeProvider.ForEachScope((scope, state) =>
		{
			state.Append("\n => ");
			state.Append(scope);
		}, sb);

		try
		{
			_testOutputHelper.WriteLine(sb.ToString());
		}
		catch (Exception)
		{
			//Ignore
		}

		Console.WriteLine(sb.ToString());
	}

	private static string GetLogLevelString(LogLevel logLevel)
	{
		return logLevel switch
		{
			LogLevel.Trace => "trce",
			LogLevel.Debug => "dbug",
			LogLevel.Information => "info",
			LogLevel.Warning => "warn",
			LogLevel.Error => "fail",
			LogLevel.Critical => "crit",
			_ => throw new ArgumentOutOfRangeException(nameof(logLevel))
		};
	}
}