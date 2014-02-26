//------------------------------------------------------------------------------
// <copyright file="CSSqlFunction.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Text;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString ConvertToIranSystem(SqlString input)
    {
        if (input == null)
        {
            return null;
        }
        else if (input.Value == string.Empty)
        {
            return null;
        }
        var value = Encoding.GetEncoding(1256).GetBytes(input.Value);
        byte[] arabicToIranSys = IranSystemConvertor.Arabic1256ToIranSystem.ArabicToIranSys(value);
        return new SqlString(CultureInfo.GetCultureInfo("en-US").LCID, SqlCompareOptions.None, arabicToIranSys, false);
    }
}
