﻿/************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2010-2012 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus edition at http://xceed.com/wpf_toolkit

   Visit http://xceed.com and follow @datagrid on Twitter

  **********************************************************************/

using System;
using System.Windows;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace Xceed.Wpf.Toolkit.PropertyGrid
{
  public class EditorDefinition
  {
    public DataTemplate EditorTemplate
    {
      get;
      set;
    }

      public ITypeEditor Editor
      {
          get;
          set;
      }

      private PropertyDefinitionCollection _properties = new PropertyDefinitionCollection();
    public PropertyDefinitionCollection PropertiesDefinitions
    {
      get
      {
        return _properties;
      }
      set
      {
        _properties = value;
      }
    }

    public Type TargetType
    {
      get;
      set;
    }
  }
}
