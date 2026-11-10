using UnityEngine;
namespace Script
{
    public class Food
    {
        private void OnCollisonEnter(Collider other)
        {
            if (!other.CompareTag("Belt"))
                return;

            Rigidbody rb = other.attachedRigidbody;

            if (rb == null)
                return;
            var belt= rb.gameObject.GetComponent<ConveyorBelt>();
        }

        public void Update()
        {
            
        }
    }
}