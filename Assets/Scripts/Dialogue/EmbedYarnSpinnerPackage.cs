using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using UnityEngine;

/// <summary>
/// This is based off the example in the Unity documentation for the
/// Unity Package Manager API, in regards to the Client.Embed function.
/// </summary>
/// Link: https://docs.unity3d.com/Manual/upm-api.html#Embed 

public static class EmbedYarnSpinnerPackage
{
    static String targetPackage = "dev.yarnspinner.unity";
    static EmbedRequest Request;
    static ListRequest LRequest;

    [MenuItem("Window/Embed Yarn Spinner Package")]
    static void GetPackageName()
    {
        // First get the name of an installed package
        LRequest = Client.List();
        EditorApplication.update += LProgress;
    }

    static void LProgress()
    {
        if (LRequest.IsCompleted)
        {
            bool doEmbed = false;
            if (LRequest.Status == StatusCode.Success)
            {
                foreach (var package in LRequest.Result)
                {
                    // Only retrieve packages that are currently installed in the
                    // project (and are neither Built-In nor already Embedded)
                    if (package.isDirectDependency 
                        && package.source != PackageSource.BuiltIn
                        && package.source != PackageSource.Embedded
                        && package.name == targetPackage)
                    {
                        doEmbed = true;
                        break;
                    }
                }

            }
            else
                Debug.Log(LRequest.Error.message);

            EditorApplication.update -= LProgress;

            if (doEmbed)
                Embed(targetPackage);

        }
    }

    static void Embed(string inTarget)
    {
        // Embed a package in the project
        Debug.Log("Embed('" + inTarget + "') called");
        Request = Client.Embed(inTarget);
        EditorApplication.update += Progress;

    }

    static void Progress()
    {
        if (Request.IsCompleted)
        {
            if (Request.Status == StatusCode.Success)
                Debug.Log("Embedded: " + Request.Result.packageId);
            else if (Request.Status >= StatusCode.Failure)
                Debug.Log(Request.Error.message);

            EditorApplication.update -= Progress;
        }
    }
}
