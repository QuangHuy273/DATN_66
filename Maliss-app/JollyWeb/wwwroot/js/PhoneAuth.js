<script>
  let appVerifier;

  function setupRecaptcha() {
    appVerifier = new firebase.auth.RecaptchaVerifier('recaptcha-container', {
      size: 'invisible'
    });
  }

  async function sendOTP(phoneNumber) {
    try {
      const confirmationResult = await firebase.auth().signInWithPhoneNumber(phoneNumber, appVerifier);
      window.confirmationResult = confirmationResult;
      return "OTP sent";
    } catch (error) {
      return "Error: " + error.message;
    }
  }

  async function verifyOTP(code) {
    try {
      const result = await window.confirmationResult.confirm(code);
      return "Verified: " + result.user.phoneNumber;
    } catch (error) {
      return "Invalid OTP: " + error.message;
    }
  }
</script>
