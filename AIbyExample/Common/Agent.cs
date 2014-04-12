using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace AIbyExample.Common
{
    public abstract class Agent
    {

        private Dictionary<string,MulticastDelegate> action=new Dictionary<string, MulticastDelegate>();

        public int Id;

        public string Name = String.Empty;

//        private IState<Agent> currentState;
//
//        public virtual IState<Agent> CurrentState {
//            get
//            {
//                return currentState;
//            }
//            set {
//                currentState = value;
//            }
//        }

        protected Agent() {
            
        }

        public Agent(string name) {
            this.Name = name;
        }

        protected virtual void BeforeUpdate() {
            
        }

        /// <summary>
        /// Turns agent to current state.
        /// </summary>
        public abstract void Update();
//            BeforeUpdate();
//            if (CurrentState != null)
//            {
//                CurrentState.Execute(this);
//            }
//            AfterUpdate();
//    }

        protected virtual void AfterUpdate() {
            
        }

//        public abstract void ChangeState(IState<Agent> state);
//        {
//            CurrentState.AfterExecuted(this);
//            CurrentState = state;
//            CurrentState.BeforeExecuting(this);
//        }

        public void Initialize() {
            
        }
    }
}
