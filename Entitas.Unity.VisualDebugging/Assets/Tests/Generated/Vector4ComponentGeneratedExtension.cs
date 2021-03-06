using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Vector4Component vector4 { get { return (Vector4Component)GetComponent(ComponentIds.Vector4); } }

        public bool hasVector4 { get { return HasComponent(ComponentIds.Vector4); } }

        static readonly Stack<Vector4Component> _vector4ComponentPool = new Stack<Vector4Component>();

        public static void ClearVector4ComponentPool() {
            _vector4ComponentPool.Clear();
        }

        public Entity AddVector4(UnityEngine.Vector4 newVector4) {
            var component = _vector4ComponentPool.Count > 0 ? _vector4ComponentPool.Pop() : new Vector4Component();
            component.vector4 = newVector4;
            return AddComponent(ComponentIds.Vector4, component);
        }

        public Entity ReplaceVector4(UnityEngine.Vector4 newVector4) {
            var previousComponent = hasVector4 ? vector4 : null;
            var component = _vector4ComponentPool.Count > 0 ? _vector4ComponentPool.Pop() : new Vector4Component();
            component.vector4 = newVector4;
            ReplaceComponent(ComponentIds.Vector4, component);
            if (previousComponent != null) {
                _vector4ComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveVector4() {
            var component = vector4;
            RemoveComponent(ComponentIds.Vector4);
            _vector4ComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherVector4;

        public static IMatcher Vector4 {
            get {
                if (_matcherVector4 == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Vector4);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherVector4 = matcher;
                }

                return _matcherVector4;
            }
        }
    }
}
