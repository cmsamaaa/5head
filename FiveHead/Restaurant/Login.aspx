<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FiveHead.Restaurant.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Staff Login | 5Head</title>
    <link rel="stylesheet" href="vendors/mdi/css/materialdesignicons.min.css" />
    <link rel="stylesheet" href="vendors/base/vendor.bundle.base.css" />
    <link rel="stylesheet" href="css/style.css">
    <link rel="shortcut icon" href="images/favicon.png" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-scroller">
            <div class="container-fluid page-body-wrapper full-page-wrapper">
                <div class="content-wrapper d-flex align-items-center auth px-0">
                    <div class="row w-100 mx-0">
                        <div class="col-lg-4 mx-auto">
                            <div class="auth-form-light text-left py-5 px-4 px-sm-5">
                                <div class="brand-logo">
                                    <img src="images/logo.svg" alt="logo">
                                </div>
                                <h4>Hello, welcome back!</h4>
                                <h6 class="font-weight-light">Sign in to continue</h6>
                                <form class="pt-3">
                                    <div class="form-group">
                                        <input runat="server" type="text" class="form-control form-control-lg" id="tb_Username" placeholder="Username" />
                                    </div>
                                    <div class="form-group">
                                        <input runat="server" type="password" class="form-control form-control-lg" id="tb_Password" placeholder="Password" />
                                    </div>
                                    <div class="mt-3">
                                        <button runat="server" id="btn_Login" class="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn" type="submit" onserverclick="btn_Login_Click">SIGN IN</button>
                                    </div>
                                    <div class="my-2 d-flex justify-content-between align-items-center">
                                        <div class="form-check">
                                            <label class="form-check-label text-muted" for="cbRememberMe">
                                                <input runat="server" id="cbRememberMe" type="checkbox" class="form-check-input" />
                                                Keep me signed in
                                            </label>
                                        </div>
                                        <a href="#" class="auth-link text-black">Forgot password?</a>
                                    </div>
                                    <div class="text-center mt-4 font-weight-light">
                                        Don't have an account? <a href="#" class="text-primary">Request</a>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- content-wrapper ends -->
            </div>
            <!-- page-body-wrapper ends -->
        </div>
        <!-- container-scroller -->
        <!-- plugins:js -->
        <script src="vendors/base/vendor.bundle.base.js"></script>
        <!-- endinject -->
        <!-- inject:js -->
        <script src="js/off-canvas.js"></script>
        <script src="js/hoverable-collapse.js"></script>
        <script src="js/template.js"></script>
        <!-- endinject -->
    </form>
</body>
</html>
