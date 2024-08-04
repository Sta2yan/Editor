using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

public class FeaturedWindow : EditorWindow
{
    private const string SaveKey = "SavedObjects";
    
    [SerializeField] private Texture2D _closeButtonIcon;
    
    private void OnEnable()
    {
        LoadObjects();
        
        var rootVisualElement = this.rootVisualElement;
        rootVisualElement.style.flexDirection = FlexDirection.Row;
        rootVisualElement.style.flexWrap = Wrap.Wrap;
        rootVisualElement.style.alignItems = Align.FlexStart;
    }

    private void OnDisable()
    {
        SaveObjects();
    }
    
    [MenuItem("Window/Featured")]
    public static FeaturedWindow ShowWindow()
    {
        FeaturedWindow window = GetWindow<FeaturedWindow>();
        window.titleContent = new GUIContent("Featured");
        return window;
    }

    private void OnGUI()
    {
        Event eventCurrent = Event.current;
        Rect dropArea = new Rect(0, 0, position.width, position.height);
        GUI.Box(dropArea, "");

        if (dropArea.Contains(eventCurrent.mousePosition))
        {
            switch (eventCurrent.type)
            {
                case EventType.DragUpdated:
                    OnEventDragUpdated();
                    break;
                case EventType.DragPerform:
                {
                    OnEventDragPerform();
                    break;
                }
            }
        }
    }

    private void OnEventDragUpdated()
    {
        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
    }

    private void OnEventDragPerform()
    {
        DragAndDrop.AcceptDrag();
        
        foreach (Object draggedObject in DragAndDrop.objectReferences) 
            AddObjectToList(draggedObject);
    }

    private void AddObjectToList(Object draggedObject)
    {
        VisualElement itemElement = CreateItemElement();

        itemElement.userData = draggedObject;

        VisualizeIconAtWindow(itemElement);
        VisualizeLabelAtWindow(itemElement);
        VisualizeRemoveButtonAtWindow(itemElement);

        // По хорошему не лямбду - нет реализации отписки
        itemElement.RegisterCallback<MouseDownEvent>(evt =>
        {
            if (evt.button == 0) 
            {
                DragAndDrop.PrepareStartDrag();
                DragAndDrop.objectReferences = new[] { draggedObject };
                DragAndDrop.StartDrag(draggedObject.name);
            }
        });
        
        rootVisualElement.Add(itemElement);
    }
    
    private void VisualizeIconAtWindow(VisualElement itemElement)
    {
        Object obj = (Object)itemElement.userData;
        
        Texture2D icon = AssetPreview.GetAssetPreview(obj) ?? AssetPreview.GetMiniThumbnail(obj);
        
        Image iconImage = new Image
        {
            image = icon,
            style =
            {
                width = 52,
                height = 52
            }
        };
        
        itemElement.Add(iconImage);
    }

    private void VisualizeLabelAtWindow(VisualElement itemElement)
    {
        Object obj = (Object)itemElement.userData;
        Label nameLabel = new Label(obj.name);
        itemElement.Add(nameLabel);
    }

    private void VisualizeRemoveButtonAtWindow(VisualElement itemElement)
    {
        // По хорошему не лямбду - нет реализации отписки
        Button removeButton = new Button(() => RemoveObject(itemElement))
        {
            style =
            {
                scale = new Vector2(1.3f, 5f),
                backgroundImage = _closeButtonIcon
            }
        };

        itemElement.Add(removeButton);
    }
    
    private VisualElement CreateItemElement()
    {
        VisualElement itemElement = new VisualElement
        {
            style =
            {
                flexDirection = FlexDirection.Row,
                alignItems = Align.Center,
                paddingBottom = 5,
                marginRight = 5
            }
        };
        
        return itemElement;
    }

    private void RemoveObject(VisualElement itemElement)
    {
        rootVisualElement.Remove(itemElement);
    }

    private void SaveObjects()
    {
        List<SavedObjectData> savedObjects = new List<SavedObjectData>();

        foreach (VisualElement child in rootVisualElement.Children())
        {
            Label nameLabel = child.Q<Label>();
            
            if (nameLabel != null)
            {
                Object objectToSave = child.userData as Object;

                SavedObjectData data = new SavedObjectData
                {
                    ObjectPath = AssetDatabase.GetAssetPath(objectToSave)
                };
                    
                savedObjects.Add(data);
            }
        }

        string json = JsonUtility.ToJson(new Serialization<SavedObjectData>(savedObjects));
        
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    private void LoadObjects()
    {
        string json = PlayerPrefs.GetString(SaveKey, string.Empty);
        
        if (string.IsNullOrEmpty(json) == false)
        {
            Serialization<SavedObjectData> loadedData = JsonUtility.FromJson<Serialization<SavedObjectData>>(json);
            
            foreach (var item in loadedData.Items)
            {
                Object loadedObject = AssetDatabase.LoadAssetAtPath(item.ObjectPath, typeof(Object));
                
                if (loadedObject != null)
                {
                    AddObjectToList(loadedObject);
                }
            }
        }
    }
}

[Serializable]
public class SavedObjectData
{
    public string ObjectPath;
}

[Serializable]
public class Serialization<T>
{
    public List<T> Items;

    public Serialization(List<T> items)
    {
        Items = items;
    }
}