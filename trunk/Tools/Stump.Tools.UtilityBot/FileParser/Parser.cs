﻿// /*************************************************************************
//  *
//  *  Copyright (C) 2010 - 2011 Stump Team
//  *
//  *  This program is free software: you can redistribute it and/or modify
//  *  it under the terms of the GNU General Public License as published by
//  *  the Free Software Foundation, either version 3 of the License, or
//  *  (at your option) any later version.
//  *
//  *  This program is distributed in the hope that it will be useful,
//  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  *  GNU General Public License for more details.
//  *
//  *  You should have received a copy of the GNU General Public License
//  *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  *
//  *************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Stump.Tools.UtilityBot.FileWriter;

namespace Stump.Tools.UtilityBot.FileParser
{
    public class AsParser
    {
        private readonly Dictionary<string, string> NameReplacementRules = new Dictionary<string, string>
            {
                {@"__AS3__\.vec\.Vector\.", @"List"},
                {@"Vector\.", @"List"},
                {@"\bError\b", @"Exception"},
                {@"push", @"Add"},
                {@"\bNumber", @"double"},
                {@"ByteArray", @"byte[]"},
                {@"\bsuper", @"base"},
                {@"\bobject\b", @"@object"},
            };

        private readonly Dictionary<string, string> BeforeParsingReplacementRules = new Dictionary
            <string, string>
            {
                {@"\.length", @".Count"},
                {@"com\.(?:[\w_]+\.)*(\w+)(\b|\s)", @"$1$2"},
                {@"\(\s*([\w\.]+)\[(\w+)\] as (\w+)\s*\)", @"$1[$2]"},
            };


        private readonly Dictionary<string, string> AfterParsingReplacementRules = new Dictionary<string, string>
            {
                {@"var (\w+) = null;", "object $1 = null;"},
                {@"new List<(\w+)>\((\w+), (?:true|false)\);", @"new List<$1>($2);"},
            };

        private readonly List<string> IgnoredLinesRules = new List<string>
            {
            };

        private string[] m_fileLines;
        private string m_fileText;

        public AsParser(string filename)
        {
            FileName = filename;

            m_fileText = File.ReadAllText(FileName);
        }

        public AsParser(string filename, bool ignoreMethods)
            : this(filename)
        {
            IgnoreMethods = ignoreMethods;   
        }

        public AsParser(string filename, bool ignoreMethods, IEnumerable<KeyValuePair<string, string>> namesReplacementsRules,
                        IEnumerable<KeyValuePair<string, string>> beforeParsingReplacementsRules,
                        IEnumerable<KeyValuePair<string, string>> afterParsingReplacementsRules,
                        IEnumerable<string> ignoredLinesRules)
            : this(filename, ignoreMethods)
        {
            foreach (var namesReplacementsRule in namesReplacementsRules)
            {
                NameReplacementRules.Add(namesReplacementsRule.Key, namesReplacementsRule.Value);
            }

            foreach (var beforeParsingReplacementsRule in beforeParsingReplacementsRules)
            {
                BeforeParsingReplacementRules.Add(beforeParsingReplacementsRule.Key, beforeParsingReplacementsRule.Value);
            }

            foreach (var afterParsingReplacementsRule in afterParsingReplacementsRules)
            {
                AfterParsingReplacementRules.Add(afterParsingReplacementsRule.Key, afterParsingReplacementsRule.Value);
            }

            IgnoredLinesRules.AddRange(ignoredLinesRules);
        }

        public AsParser(string filename, IEnumerable<KeyValuePair<string, string>> namesReplacementsRules,
                        IEnumerable<KeyValuePair<string, string>> beforeParsingReplacementsRules,
                        IEnumerable<KeyValuePair<string, string>> afterParsingReplacementsRules,
                        IEnumerable<string> ignoredLinesRules)
            : this(filename, false, namesReplacementsRules, beforeParsingReplacementsRules, afterParsingReplacementsRules, ignoredLinesRules)
        {

        }

        public ClassInfo Class
        {
            get;
            internal set;
        }

        public MethodInfo Constructor
        {
            get;
            internal set;
        }

        public IEnumerable<IExecution> ConstructorElements
        {
            get;
            internal set;
        }

        public List<FieldInfo> Fields
        {
            get;
            internal set;
        }

        public List<MethodInfo> Methods
        {
            get;
            internal set;
        }

        public Dictionary<MethodInfo, IEnumerable<IExecution>> MethodsElements
        {
            get;
            internal set;
        }

        public string FileName
        {
            get;
            set;
        }

        public bool IgnoreMethods
        {
            get;
            set;
        }

        public void ParseFile()
        {
            m_fileText = ExecuteBeforeParsingReplacement(m_fileText);
            m_fileLines = m_fileText.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            Class = new ClassInfo
                {
                    Name = ExecuteNameReplacement(GetMatch(@"public class (\w+)\s")),
                    Heritage = ExecuteNameReplacement(GetMatch(@"extends (?:[\w_]\.)*(\w+)\s")),
                    AccessModifier = AccessModifiers.PUBLIC,
                    // we don't mind about this
                    ClassModifier = ClassInfo.ClassModifiers.NONE
                };

            if (!IgnoreMethods)
                ParseConstructor();

            ParseFields();

            if (!IgnoreMethods)
                ParseMethods();
        }

        private void ParseConstructor()
        {
            Match matchConstructor = Regex.Match(m_fileText,
                                                 string.Format(@"(?<acces>public|protected|private|internal)\s*function\s*(?<name>{0})\((?<argument>[^,)]+,?)*\)", Class.Name));

            if (matchConstructor.Success)
            {
                Constructor = BuildMethodInfoFromMatch(matchConstructor, true);
                ConstructorElements = BuildMethodElementsFromMatch(matchConstructor);
            }
        }

        private void ParseFields()
        {
            Fields = new List<FieldInfo>();

            Match matchConst = Regex.Match(m_fileText,
                                           @"(?<acces>public|protected|private|internal)\s*(?<static>static)?\s*const\s*(?<name>\w+):(?<type>[\w_\.]+(?:<(?:\w+\.)*(?<generictype>\w+)>)?)(?<value>\s*=\s*.*)?;");
            while (matchConst.Success)
            {
                var field = new FieldInfo
                    {
                        Modifiers =
                            (AccessModifiers)
                            Enum.Parse(typeof(AccessModifiers),
                                       ExecuteNameReplacement(matchConst.Groups["acces"].Value),
                                       true),
                        Name = ExecuteNameReplacement(matchConst.Groups["name"].Value),
                        Type =
                            ExecuteNameReplacement(matchConst.Groups["generictype"].Value == ""
                                                       ? matchConst.Groups["type"].Value
                                                       : "List<" + matchConst.Groups["generictype"].Value + ">"),
                        Value = ExecuteNameReplacement(matchConst.Groups["value"].Value.Replace("=", "").Trim()),
                        Stereotype = "const",
                    };

                Fields.Add(field);

                matchConst = matchConst.NextMatch();
            }

            Match matchVar = Regex.Match(m_fileText,
                                         @"(?<acces>public|protected|private|internal)\s*(?<static>static)?\s*var\s*(?<name>\w+):(?<type>[\w_\.]+(?:<(?:\w+\.)*(?<generictype>\w+)>)?)(?<value>\s*=\s*.*)?;");
            while (matchVar.Success)
            {
                var field = new FieldInfo
                    {
                        Modifiers =
                            (AccessModifiers)
                            Enum.Parse(typeof(AccessModifiers), ExecuteNameReplacement(matchVar.Groups["acces"].Value),
                                       true),
                        Name = ExecuteNameReplacement(matchVar.Groups["name"].Value),
                        Type =
                            ExecuteNameReplacement(matchVar.Groups["generictype"].Value == ""
                                                       ? matchVar.Groups["type"].Value
                                                       : "List<" + matchVar.Groups["generictype"].Value + ">"),
                        Value = ExecuteNameReplacement(matchVar.Groups["value"].Value.Replace("=", "").Trim()),
                        Stereotype = ExecuteNameReplacement(matchConst.Groups["static"].Value)
                    };

                Fields.Add(field);

                matchVar = matchVar.NextMatch();
            }
        }

        private void ParseMethods()
        {
            Methods = new List<MethodInfo>();
            MethodsElements = new Dictionary<MethodInfo, IEnumerable<IExecution>>();

            Match matchMethods = Regex.Match(m_fileText,
                                             @"(?<acces>public|protected|private|internal)\s*(?<override>override)?\s*function\s*(?<name>\w+)\((?<argument>[^,)]+,?)*\):(?:\w+\.)*(?<=\.|:)(?<returntype>\w+)");
            while (matchMethods.Success)
            {
                MethodInfo method = BuildMethodInfoFromMatch(matchMethods, false);

                Methods.Add(method);
                MethodsElements.Add(method, BuildMethodElementsFromMatch(matchMethods));

                matchMethods = matchMethods.NextMatch();
            }
        }

        private MethodInfo BuildMethodInfoFromMatch(Match match, bool constructor)
        {
            var method = new MethodInfo
                {
                    AccessModifier =
                        (AccessModifiers)
                        Enum.Parse(typeof(AccessModifiers),
                                   ExecuteNameReplacement(match.Groups["acces"].Value), true),
                    Name = ExecuteNameReplacement(match.Groups["name"].Value),
                    Modifiers = match.Groups["override"].Value == "override"
                                    ? new List<MethodInfo.MethodModifiers>(new[] { MethodInfo.MethodModifiers.OVERRIDE })
                                    : new List<MethodInfo.MethodModifiers>(new[] { MethodInfo.MethodModifiers.NONE }),
                    ReturnType = constructor ? "" : ExecuteNameReplacement(match.Groups["returntype"].Value),
                    ReturnsArray = false
                };

            // todo : hard code, find a better way
            if (( method.Name == "reset" ||
                 method.Name == "getTypeId" ||
                 method.Name == "serialize" ||
                 method.Name == "deserialize" ) &&
                method.Modifiers.Contains(MethodInfo.MethodModifiers.NONE))
            {
                method.Modifiers.Clear();
                method.Modifiers.Add(MethodInfo.MethodModifiers.VIRTUAL);
            }


            var args = new List<string>();
            var argsType = new List<string>();
            var argsDefaultValue = new List<string>();
            foreach (object capture in match.Groups["argument"].Captures)
            {
                string arg = ExecuteNameReplacement(capture.ToString().Trim().Replace(",", ""));

                args.Add(arg.Split(':').First());

                string type = "";
                if (arg.Contains("<"))
                {
                    string generictype = arg.Split('<').Last().Split('>').First().Split('.').Last();

                    type = "List<" + generictype + ">";
                }
                else
                    type = arg.Split(':').Last().Split('.').Last();

                string defaultValue = null;
                if (type.Contains("="))
                {
                    defaultValue = type.Split('=').Last();
                    type = type.Split('=').First();
                }
                else if (argsDefaultValue.LastOrDefault() != null)
                {
                    defaultValue = "null";
                }

                argsType.Add(ExecuteNameReplacement(type));
                argsDefaultValue.Add(ExecuteNameReplacement(defaultValue));
            }
            method.Args = args.ToArray();
            method.ArgsType = argsType.ToArray();
            method.ArgsDefaultValue = argsDefaultValue.ToArray();

            return method;
        }

        private IEnumerable<IExecution> BuildMethodElementsFromMatch(Match match)
        {
            int lineBegin =
                Array.FindIndex(m_fileLines, (entry) => entry.Contains(match.Groups[0].Value)) + 1;
            int lineEnd = 0;

            string beginBracket = m_fileLines[lineBegin];
            string endBracket = beginBracket.Remove(beginBracket.Length - 1, 1) + "}";

            for (int i = lineBegin; i < m_fileLines.Length; i++)
            {
                if (m_fileLines[i] == endBracket)
                {
                    lineEnd = i;
                    break;
                }
            }

            var methodlines = new string[lineEnd - lineBegin];
            Array.Copy(m_fileLines, lineBegin, methodlines, 0, lineEnd - lineBegin);

            return ParseMethodExecutions(methodlines);
        }

        private IEnumerable<IExecution> ParseMethodExecutions(IEnumerable<string> lines)
        {
            var result = new List<IExecution>();

            int controlsequenceDepth = 0;
            foreach (string line in lines.Select(entry => entry.Trim()))
            {
                if (IgnoredLinesRules.Contains(line))
                    continue;

                if (line == "{")
                    continue;
                else if (line == "}")
                {
                    if (controlsequenceDepth > 0)
                    {
                        result.Add(new ControlSequenceEnd());
                        controlsequenceDepth--;
                    }

                    continue;
                }

                IExecution execution;

                if (Regex.IsMatch(line, ControlSequence.Pattern))
                {
                    execution = ControlSequence.Parse(ExecuteNameReplacement(line));
                    controlsequenceDepth++;
                }

                else if (Regex.IsMatch(line, FunctionCall.Pattern))
                {
                    execution = FunctionCall.Parse(line, this);
                    if (!string.IsNullOrEmpty(( execution as FunctionCall ).ReturnVariableAssignation) &&
                        string.IsNullOrEmpty(( execution as FunctionCall ).Stereotype) &&
                        Fields.Count(entry => entry.Name == ( (FunctionCall)execution ).ReturnVariableAssignation) > 0)
                    {
                        ( execution as FunctionCall ).Stereotype = "(" +
                                                                 Fields.Where(
                                                                     entry =>
                                                                     entry.Name ==
                                                                     ( (FunctionCall)execution ).
                                                                         ReturnVariableAssignation).First().Type + ")";
                    }

                    // cast to generic type
                    if (!string.IsNullOrEmpty(( execution as FunctionCall ).Target) &&
                        ( execution as FunctionCall ).Name == "Add" &&
                        Fields.Count(entry => entry.Name == ( (FunctionCall)execution ).Target.Split('.').Last()) > 0)
                    {
                        string generictype =
                            Fields.Where(entry => entry.Name == ( (FunctionCall)execution ).Target.Split('.').Last()).
                                First().Type.Split('<').Last().Split('>').First();

                        ( execution as FunctionCall ).Args[0] = "(" + generictype + ")" +
                                                              ( execution as FunctionCall ).Args[0];
                    }
                }

                else if (Regex.IsMatch(line, VariableAssignation.Pattern))
                {
                    execution = VariableAssignation.Parse(line, this);
                }

                else
                    execution = new Unknown
                        {
                            Execution = line
                        };

                result.Add(execution);
            }

            return result;
        }

        public void ToCSharp(string destFileName, string @namespace, IEnumerable<string> usings)
        {
            var writer = new CsFileWriter(destFileName, usings);

            Class.Namespace = @namespace;
            writer.StartClass(Class);

            foreach (FieldInfo field in Fields)
            {
                writer.WriteField(field.Modifiers, field.Type, field.Name, field.Value, field.Stereotype);
            }

            writer.WriteLineWithIndent();

            // constructor
            if (Constructor != null)
            {
                writer.StartMethod(Constructor);

                foreach (IExecution element in ConstructorElements)
                {
                    if (element.Type == ExecutionType.ControlSequence)
                    {
                        writer.StartControlSequence(( (ControlSequence)element ).SequenceType,
                                                    ( (ControlSequence)element ).Condition);
                    }
                    else if (element.Type == ExecutionType.ControlSequenceEnd)
                    {
                        writer.EndControlSequence();
                    }
                    else if (element.Type == ExecutionType.FunctionCall)
                    {
                        writer.WriteExecution(( (FunctionCall)element ).Target,
                                              ( (FunctionCall)element ).Name,
                                              ( (FunctionCall)element ).ReturnVariableAssignation,
                                              ( (FunctionCall)element ).ReturnVariableTypeAssignation,
                                              ( (FunctionCall)element ).ReturnVariableAssignationTarget,
                                              ( (FunctionCall)element ).Args,
                                              ( (FunctionCall)element ).Stereotype);
                    }
                    else if (element.Type == ExecutionType.VariableAssignation)
                    {
                        writer.WriteVariableAssignation(( (VariableAssignation)element ).Target,
                                                        ( (VariableAssignation)element ).Name,
                                                        ( (VariableAssignation)element ).Value,
                                                        ( (VariableAssignation)element ).TypeDeclaration);
                    }
                    else
                        writer.WriteCustom(( (Unknown)element ).Execution);
                }

                writer.EndMethod();
            }

            // generate a constructor based on the init function
            if (Methods != null &&
                Methods.Count(entry => entry.Name.StartsWith("init")) == 1)
            {
                MethodInfo methodInit = Methods.Where(entry => entry.Name.StartsWith("init")).First();

                if (methodInit.Args.Length > 0)
                {
                    var customConstructor = new MethodInfo
                        {
                            AccessModifier = AccessModifiers.PUBLIC,
                            Name = Class.Name,
                            Modifiers = new List<MethodInfo.MethodModifiers>(new[] { MethodInfo.MethodModifiers.NONE }),
                            ReturnType = "",
                            ReturnsArray = false,
                            Args = methodInit.Args,
                            ArgsDefaultValue = new string[methodInit.ArgsDefaultValue.Length],
                            ArgsType = methodInit.ArgsType
                        };

                    var elements = new List<IExecution>
                        {
                            new FunctionCall
                                {
                                    Args = methodInit.Args,
                                    Name = methodInit.Name,
                                    ReturnVariableAssignation = "",
                                    ReturnVariableAssignationTarget = "",
                                    ReturnVariableTypeAssignation = "",
                                    Stereotype = "",
                                    Target = ""
                                }
                        };

                    writer.StartMethod(customConstructor, ": this()");

                    foreach (IExecution element in elements)
                    {
                        if (element.Type == ExecutionType.ControlSequence)
                        {
                            writer.StartControlSequence(( (ControlSequence)element ).SequenceType,
                                                        ( ( (ControlSequence)element ) ).Condition);
                        }
                        else if (element.Type == ExecutionType.ControlSequenceEnd)
                        {
                            writer.EndControlSequence();
                        }
                        else if (element.Type == ExecutionType.FunctionCall)
                        {
                            writer.WriteExecution(( (FunctionCall)element ).Target,
                                                  ( ( (FunctionCall)element ) ).Name,
                                                  ( ( (FunctionCall)element ) ).ReturnVariableAssignation,
                                                  ( ( (FunctionCall)element ) ).ReturnVariableTypeAssignation,
                                                  ( ( (FunctionCall)element ) ).ReturnVariableAssignationTarget,
                                                  ( ( (FunctionCall)element ) ).Args,
                                                  ( ( (FunctionCall)element ) ).Stereotype);
                        }
                        else if (element.Type == ExecutionType.VariableAssignation)
                        {
                            writer.WriteVariableAssignation(( (VariableAssignation)element ).Target,
                                                            ( (VariableAssignation)element ).Name,
                                                            ( (VariableAssignation)element ).Value,
                                                            ( (VariableAssignation)element ).TypeDeclaration);
                        }
                        else
                            writer.WriteCustom(( (Unknown)element ).Execution);
                    }

                    writer.EndMethod();
                }
            }

            if (Methods != null)
                foreach (MethodInfo method in Methods)
                {
                    writer.StartMethod(method);

                    foreach (IExecution element in MethodsElements[method])
                    {
                        if (element.Type == ExecutionType.ControlSequence)
                        {
                            writer.StartControlSequence(( (ControlSequence)element ).SequenceType,
                                                        ( (ControlSequence)element ).Condition);
                        }
                        else if (element.Type == ExecutionType.ControlSequenceEnd)
                        {
                            writer.EndControlSequence();
                        }
                        else if (element.Type == ExecutionType.FunctionCall)
                        {
                            writer.WriteExecution(( (FunctionCall)element ).Target,
                                                  ( (FunctionCall)element ).Name,
                                                  ( (FunctionCall)element ).ReturnVariableAssignation,
                                                  ( (FunctionCall)element ).ReturnVariableTypeAssignation,
                                                  ( (FunctionCall)element ).ReturnVariableAssignationTarget,
                                                  ( (FunctionCall)element ).Args,
                                                  ( (FunctionCall)element ).Stereotype);
                        }
                        else if (element.Type == ExecutionType.VariableAssignation)
                        {
                            writer.WriteVariableAssignation(( (VariableAssignation)element ).Target,
                                                            ( (VariableAssignation)element ).Name,
                                                            ( (VariableAssignation)element ).Value,
                                                            ( (VariableAssignation)element ).TypeDeclaration);
                        }
                        else
                            writer.WriteCustom(( (Unknown)element ).Execution);
                    }

                    writer.EndMethod();
                }

            writer.EndClass();

            File.WriteAllText(destFileName, ExecuteAfterParsingReplacement(File.ReadAllText(destFileName)));
        }


        private string GetMatch(string pattern)
        {
            try
            {
                Match match = Regex.Match(m_fileLines.Where(entry => Regex.IsMatch(entry, pattern)).First(), pattern);

                return match.Groups[1].Value;
            }
            catch
            {
                return "";
            }
        }

        internal string ExecuteNameReplacement(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            foreach (var rule in NameReplacementRules)
            {
                if (str.Contains(rule.Key) || Regex.IsMatch(str, rule.Key))
                    str = Regex.Replace(str, rule.Key, rule.Value);
            }

            return str;
        }

        internal string ExecuteBeforeParsingReplacement(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            foreach (var rule in BeforeParsingReplacementRules)
            {
                if (str.Contains(rule.Key) || Regex.IsMatch(str, rule.Key))
                    str = Regex.Replace(str, rule.Key, rule.Value);
            }

            return str;
        }

        internal string ExecuteAfterParsingReplacement(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            foreach (var rule in AfterParsingReplacementRules)
            {
                if (str.Contains(rule.Key) || Regex.IsMatch(str, rule.Key))
                    str = Regex.Replace(str, rule.Key, rule.Value);
            }

            return str;
        }
    }
}