using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace UT4MasterServer.Models;

public class ErrorResponse
{
	[JsonProperty("errorCode")]
	public string? ErrorCode { get; set; }

	[JsonProperty("errorMessage")]
	public string? ErrorMessage { get; set; }

	[JsonProperty("messageVars")]
	public string[] MessageVars { get; set; } = Array.Empty<string>(); // any value inside errorMessage is listed in this array

	[JsonProperty("numericErrorCode")]
	public int NumericErrorCode { get; set; }

	[JsonProperty("originatingService")]
	public string? OriginatingService { get; set; }

	[JsonProperty("intent")]
	public string? Intent { get; set; }

	// TODO: Use one JSON DLL in solution. See https://github.com/timiimit/UT4MasterServer/issues/33
	[JsonPropertyName("error_description")] // Fix for API response
	[JsonProperty("error_description")]
	public string? ErrorDescription { get; set; }

	[JsonProperty("error")]
	public string? Error { get; set; }

	public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
	{
		return new Task<HttpResponseMessage>(() => new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));
	}

	/*
	Some examples:

	1.
	{
	    "errorCode":"errors.com.epicgames.account.auth_token.invalid_refresh_token",
	    "errorMessage":"Sorry the refresh token '971A38DCCBBA60E51F4AB04A09BE7B3F3D9D983C8814D279CF41321C8D3906B5' is invalid",
	    "messageVars":[
	        "971A38DCCBBA60E51F4AB04A09BE7B3F3D9D983C8814D279CF41321C8D3906B5"
	    ],
	    "numericErrorCode":18036,
	    "originatingService":"com.epicgames.account.public",
	    "intent":"prod",
	    "error_description":"Sorry the refresh token '971A38DCCBBA60E51F4AB04A09BE7B3F3D9D983C8814D279CF41321C8D3906B5' is invalid",
	    "error":"invalid_grant"
	}

	2.
	{
	    "errorCode": "errors.com.epicgames.common.oauth.unsupported_grant_type",
	    "errorMessage": "Unsupported grant type: testGrantType",
	    "messageVars": [],
	    "numericErrorCode": 1016,
	    "originatingService": "com.epicgames.account.public",
	    "intent": "prod",
	    "error_description": "Unsupported grant type: testGrantType",
	    "error": "unsupported_grant_type"
	}

	or: "Unsupported grant type: ",
	or: "errorMessage": "Unsupported grant type: null", (when we have valid body, but not have `grant_type` key)

	3.
	{
	    "errorCode": "errors.com.epicgames.common.oauth.invalid_grant",
	    "errorMessage": "It appears that you have not usable included any data in the body of your request. Please verify that you are sending a application/x-www-form-urlencoded body",
	    "messageVars": [],
	    "numericErrorCode": 1012,
	    "originatingService": "com.epicgames.account.public",
	    "intent": "prod",
	    "error_description": "It appears that you have not usable included any data in the body of your request. Please verify that you are sending a application/x-www-form-urlencoded body",
	    "error": "invalid_grant"
	}

	4.
	{
	    "errorCode": "errors.com.epicgames.common.unsupported_media_type",
	    "errorMessage": "Sorry your request could not be processed as you are supplying a media type we do not support.",
	    "numericErrorCode": 1006,
	    "originatingService": "com.epicgames.account.public",
	    "intent": "prod"
	}

	5.
	{
	    "errorCode": "errors.com.epicgames.common.method_not_allowed",
	    "errorMessage": "Sorry the resource you were trying to access cannot be accessed with the HTTP method you used.",
	    "numericErrorCode": 1009,
	    "originatingService": "com.epicgames.account.public",
	    "intent": "prod"
	}

	6.
	{
	    "errorCode": "errors.com.epicgames.common.authentication.authentication_failed",
	    "errorMessage": "Authentication failed for /api/oauth/verify",
	    "messageVars": [
	        "/api/oauth/verify"
	    ],
	    "numericErrorCode": 1032,
	    "originatingService": "com.epicgames.account.public",
	    "intent": "prod"
	}

	7.
	{
	    "errorCode": "errors.com.epicgames.common.oauth.invalid_token",
	    "errorMessage": "Malformed auth token [invalidBearerToken]",
	    "messageVars": [
	        "invalidBearerToken"
	    ],
	    "numericErrorCode": 1014,
	    "originatingService": "com.epicgames.account.public",
	    "intent": "prod"
	}

	or: "The OAuthToken you are using is not valid"
	*/
}
