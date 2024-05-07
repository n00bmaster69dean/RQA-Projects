using U4K.BehaviourTemplate.Attr;
using UnityEngine;

namespace U4K.BehaviourTemplate.Action
{
    public class PlayAudioAction : BasicAction
    {
        [SerializeField] [FieldShownAs("audio clip")]
        private AudioClip audioClip = null;

        [SerializeField] private bool loop = false;

        public override void Execute()
        {
            base.Execute();
            if (!loop) _done = false;
            if (!_done && actor != null && audioClip != null)
            {
                var source = actor.GetComponent<AudioSource>();
                if (source == null || source.isPlaying)
                {
                    source = actor.AddComponent<AudioSource>();
                }

                source.loop = loop;
                source.clip = audioClip;
                source.Play();

                _done = true;
            }
        }

        public new static string Name
        {
            get { return "Play Audio"; }
        }
    }
}