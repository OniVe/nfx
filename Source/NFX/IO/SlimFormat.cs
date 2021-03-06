/*<FILE_LICENSE>
* NFX (.NET Framework Extension) Unistack Library
* Copyright 2003-2017 ITAdapter Corp. Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
</FILE_LICENSE>*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFX.IO
{
    /// <summary>
    /// A format that writes into binary files in an efficient way using variable-length integers, strings and meta handles.
    /// Developers may derive new formats that support custom serialization of their business-related types. This may increase performance dramatically.
    /// For example, in a drawing application a new format may derive from SlimFormat to natively serialize Point and PolarPoint structs to yield faster serialization times.
    /// NFX.Serialization.Slim.SlimSlimSerializer is capable of SlimFormat-derived format injection, in which case it will automatically discover new types that are directly supported
    /// by the format.
    /// </summary>
    public class SlimFormat : StreamerFormat<SlimReader, SlimWriter>
    {
       public const int MAX_BYTE_ARRAY_LEN =  512 * //mb
                                             1024 * //kb
                                             1024;

       public const int MAX_INT_ARRAY_LEN =  MAX_BYTE_ARRAY_LEN / 4;

       public const int MAX_LONG_ARRAY_LEN =  MAX_BYTE_ARRAY_LEN / 8;

       public const int MAX_DOUBLE_ARRAY_LEN =  MAX_BYTE_ARRAY_LEN / 8;

       public const int MAX_FLOAT_ARRAY_LEN =  MAX_BYTE_ARRAY_LEN / 4;

       public const int MAX_DECIMAL_ARRAY_LEN =  MAX_BYTE_ARRAY_LEN / 13;

       public const int MAX_STRING_ARRAY_CNT =  MAX_BYTE_ARRAY_LEN / 48;


       protected SlimFormat() : base()
       {
          TypeSchema = new Serialization.Slim.TypeSchema(this);
       }

       private static SlimFormat s_Instance = new SlimFormat();

       /// <summary>
       /// Returns a singleton format instance
       /// </summary>
       public static SlimFormat Instance
       {
         get { return s_Instance;}
       }


       public override Type ReaderType
       {
           get { return typeof(SlimReader); }
       }

       public override Type WriterType
       {
           get { return typeof(SlimWriter); }
       }


       /// <summary>
       /// Internally references type schema
       /// </summary>
       internal readonly Serialization.Slim.TypeSchema TypeSchema;


       public override SlimReader MakeReadingStreamer(Encoding encoding = null)
       {
           return new SlimReader(encoding);
       }

       public override SlimWriter MakeWritingStreamer(Encoding encoding = null)
       {
           return new SlimWriter(encoding);
       }
    }
}
