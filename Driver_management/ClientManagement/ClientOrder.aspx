<%@ Page Title="" Language="C#" MasterPageFile="~/ClientManagement/ClientMaster.Master" AutoEventWireup="true" CodeBehind="ClientOrder.aspx.cs" Inherits="Driver_management.ClientManagement.ClientOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" integrity="sha384-JcKb8q3iqJ61gNV9KGb8thSsNjpSL0n8PARn9HuZOnIxN0hoP+VmmDGMN5t9UJ0Z" crossorigin="anonymous">
    <style>
        .card-modern {
            border-radius: 15px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }
        .card-header-modern {
            background: linear-gradient(to right, #0062E6, #33AEFF);
            border-radius: 15px 15px 0 0;
        }
        .btn-modern {
            background: linear-gradient(to right, #33AEFF, #0062E6);
            border: none;
            transition: background 0.3s ease;
        }
        .btn-modern:hover {
            background: linear-gradient(to right, #0062E6, #33AEFF);
        }
        .terms-conditions {
            max-height: 500px;
            overflow-y: auto;
            border: 1px solid #ddd;
            padding: 15px;
            border-radius: 5px;
            background-color: #f9f9f9;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-lg-6">
                <div class="card card-modern">
                    <div class="card-header card-header-modern text-white text-center">
                        <h2 class="card-title mb-0">פרטי הזמנה</h2>
                    </div>
                    <div class="card-body p-4">
                        <div class="form-group">
                            <asp:Label ID="LblDestinationAddress" runat="server" AssociatedControlID="TxtDestinationAddress" Text="כתובת יעד:" CssClass="font-weight-bold"></asp:Label>
                            <asp:TextBox ID="TxtDestinationAddress" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="LblNumberOfPackages" runat="server" AssociatedControlID="TxtNumberOfPackages" Text="מספר חבילות:" CssClass="font-weight-bold"></asp:Label>
                            <asp:TextBox ID="TxtNumberOfPackages" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtNumberOfPackages_TextChanged"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="LblOrderDate" runat="server" AssociatedControlID="TxtOrderDate" Text="תאריך הזמנה:" CssClass="font-weight-bold"></asp:Label>
                            <asp:TextBox ID="TxtOrderDate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="LblCityDestination" runat="server" AssociatedControlID="DdlCityDestination" Text="עיר יעד:" CssClass="font-weight-bold"></asp:Label>
                            <asp:DropDownList ID="DdlCityDestination" runat="server" CssClass="form-control">
                                <asp:ListItem Text="בחר עיר..." Value=""></asp:ListItem>
                                <%-- Add city options here --%>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="LblTotalAmount" runat="server" Text="סכום לתשלום: " CssClass="font-weight-bold"></asp:Label>
                            <asp:Label ID="LblTotalAmountValue" runat="server" Text="0" CssClass="font-weight-bold"></asp:Label>
                        </div>
                        <div class="form-group text-center">
                            <asp:Button ID="BtnSubmit" runat="server" Text="שלח" OnClick="BtnSubmit_Click" CssClass="btn btn-modern btn-lg text-white" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="terms-conditions">
                    <h1>Terms and Conditions</h1>
                    <p>Last updated: June 08, 2024</p>
                    <p>Please read these terms and conditions carefully before using Our Service.</p>
                    <h2>Interpretation and Definitions</h2>
                    <h3>Interpretation</h3>
                    <p>The words of which the initial letter is capitalized have meanings defined under the following conditions. The following definitions shall have the same meaning regardless of whether they appear in singular or in plural.</p>
                    <h3>Definitions</h3>
                    <p>For the purposes of these Terms and Conditions:</p>
                    <ul>
                        <li><strong>Affiliate</strong> means an entity that controls, is controlled by or is under common control with a party, where "control" means ownership of 50% or more of the shares, equity interest or other securities entitled to vote for election of directors or other managing authority.</li>
                        <li><strong>Country</strong> refers to: Israel</li>
                        <li><strong>Company</strong> (referred to as either "the Company", "We", "Us" or "Our" in this Agreement) refers to SocialMedia.</li>
                        <li><strong>Device</strong> means any device that can access the Service such as a computer, a cellphone or a digital tablet.</li>
                        <li><strong>Service</strong> refers to the Website.</li>
                        <li><strong>Terms and Conditions</strong> (also referred as "Terms") mean these Terms and Conditions that form the entire agreement between You and the Company regarding the use of the Service. This Terms and Conditions agreement has been created with the help of the <a href="https://www.termsfeed.com/terms-conditions-generator/" target="_blank">Terms and Conditions Generator</a>.</li>
                        <li><strong>Third-party Social Media Service</strong> means any services or content (including data, information, products or services) provided by a third-party that may be displayed, included or made available by the Service.</li>
                        <li><strong>Website</strong> refers to SocialMedia, accessible from <a href="http://localhost:11263/" rel="external nofollow noopener" target="_blank">http://localhost:11263/</a></li>
                        <li><strong>You</strong> means the individual accessing or using the Service, or the company, or other legal entity on behalf of which such individual is accessing or using the Service, as applicable.</li>
                    </ul>
                    <h2>Acknowledgment</h2>
                    <p>These are the Terms and Conditions governing the use of this Service and the agreement that operates between You and the Company. These Terms and Conditions set out the rights and obligations of all users regarding the use of the Service.</p>
                    <p>Your access to and use of the Service is conditioned on Your acceptance of and compliance with these Terms and Conditions. These Terms and Conditions apply to all visitors, users and others who access or use the Service.</p>
                    <p>By accessing or using the Service You agree to be bound by these Terms and Conditions. If You disagree with any part of these Terms and Conditions then You may not access the Service.</p>
                    <p>You represent that you are over the age of 18. The Company does not permit those under 18 to use the Service.</p>
                    <p>Your access to and use of the Service is also conditioned on Your acceptance of and compliance with the Privacy Policy of the Company. Our Privacy Policy describes Our policies and procedures on the collection, use and disclosure of Your personal information when You use the Application or the Website and tells You about Your privacy rights and how the law protects You. Please read Our Privacy Policy carefully before using Our Service.</p>
                    <h2>Links to Other Websites</h2>
                    <p>Our Service may contain links to third-party web sites or services that are not owned or controlled by the Company.</p>
                    <p>The Company has no control over, and assumes no responsibility for, the content, privacy policies, or practices of any third party web sites or services. You further acknowledge and agree that the Company shall not be responsible or liable, directly or indirectly, for any damage or loss caused or alleged to be caused by or in connection with the use of or reliance on any such content, goods or services available on or through any such web sites or services.</p>
                    <p>We strongly advise You to read the terms and conditions and privacy policies of any third-party web sites or services that You visit.</p>
                    <h2>Termination</h2>
<p>We may terminate or suspend Your access immediately, without prior notice or liability, for any reason whatsoever, including without limitation if You breach these Terms and Conditions.</p>
<p>Upon termination, Your right to use the Service will cease immediately.</p>
<h2>Limitation of Liability</h2>
<p>Notwithstanding any damages that You might incur, the entire liability of the Company and any of its suppliers under any provision of this Terms and Your exclusive remedy for all of the foregoing shall be limited to the amount actually paid by You through the Service or 100 USD if You haven't purchased anything through the Service.</p>
<p>To the maximum extent permitted by applicable law, in no event shall the Company or its suppliers be liable for any special, incidental, indirect, or consequential damages whatsoever (including, but not limited to, damages for loss of profits, loss of data or other information, for business interruption, for personal injury, loss of privacy arising out of or in any way related to the use of or inability to use the Service, third-party software and/or third-party hardware used with the Service, or otherwise in connection with any provision of this Terms), even if the Company or any supplier has been advised of the possibility of such damages and even if the remedy fails of its essential purpose.</p>
<p>Some states do not allow the exclusion of implied warranties or limitation of liability for incidental or consequential damages, which means that some of the above limitations may not apply. In these states, each party's liability will be limited to the greatest extent permitted by law.</p>
<h2>&quot;AS IS&quot; and &quot;AS AVAILABLE&quot; Disclaimer</h2>
<p>The Service is provided to You &quot;AS IS&quot; and &quot;AS AVAILABLE&quot; and with all faults and defects without warranty of any kind. To the maximum extent permitted under applicable law, the Company, on its own behalf and on behalf of its Affiliates and its and their respective licensors and service providers, expressly disclaims all warranties, whether express, implied, statutory or otherwise, with respect to the Service, including all implied warranties of merchantability, fitness for a particular purpose, title and non-infringement, and warranties that may arise out of course of dealing, course of performance, usage or trade practice. Without limitation to the foregoing, the Company provides no warranty or undertaking, and makes no representation of any kind that the Service will meet Your requirements, achieve any intended results, be compatible or work with any other software, applications, systems or services, operate without interruption, meet any performance or reliability standards or be error free or that any errors or defects can or will be corrected.</p>
<p>Without limiting the foregoing, neither the Company nor any of the company's provider makes any representation or warranty of any kind, express or implied: (i) as to the operation or availability of the Service, or the information, content, and materials or products included thereon; (ii) that the Service will be uninterrupted or error-free; (iii) as to the accuracy, reliability, or currency of any information or content provided through the Service; or (iv) that the Service, its servers, the content, or e-mails sent from or on behalf of the Company are free of viruses, scripts, trojan horses, worms, malware, timebombs or other harmful components.</p>
<p>Some jurisdictions do not allow the exclusion of certain types of warranties or limitations on applicable statutory rights of a consumer, so some or all of the above exclusions and limitations may not apply to You. But in such a case the exclusions and limitations set forth in this section shall be applied to the greatest extent enforceable under applicable law.</p>
<h2>Governing Law</h2>
<p>The laws of the Country, excluding its conflicts of law rules, shall govern this Terms and Your use of the Service. Your use of the Application may also be subject to other local, state, national, or international laws.</p>
<h2>Disputes Resolution</h2>
<p>If You have any concern or dispute about the Service, You agree to first try to resolve the dispute informally by contacting the Company.</p>
<h2>For European Union (EU) Users</h2>
<p>If You are a European Union consumer, you will benefit from any mandatory provisions of the law of the country in which You are resident.</p>
<h2>United States Legal Compliance</h2>
<p>You represent and warrant that (i) You are not located in a country that is subject to the United States government embargo, or that has been designated by the United States government as a &quot;terrorist supporting&quot; country, and (ii) You are not listed on any United States government list of prohibited or restricted parties.</p>
<h2>Severability and Waiver</h2>
<h3>Severability</h3>
<p>If any provision of these Terms is held to be unenforceable or invalid, such provision will be changed and interpreted to accomplish the objectives of such provision to the greatest extent possible under applicable law and the remaining provisions will continue in full force and effect.</p>
<h3>Waiver</h3>
<p>Except as provided herein, the failure to exercise a right or to require performance of an obligation under these Terms shall not affect a party's ability to exercise such right or require such performance at any time thereafter nor shall the waiver of a breach constitute a waiver of any subsequent breach.</p>
<h2>Translation Interpretation</h2>
<p>These Terms and Conditions may have been translated if We have made them available to You on our Service.
You agree that the original English text shall prevail in the case of a dispute.</p>
<h2>Changes to These Terms and Conditions</h2>
<p>We reserve the right, at Our sole discretion, to modify or replace these Terms at any time. If a revision is material We will make reasonable efforts to provide at least 30 days' notice prior to any new terms taking effect. What constitutes a material change will be determined at Our sole discretion.</p>
<p>By continuing to access or use Our Service after those revisions become effective, You agree to be bound by the revised terms. If You do not agree to the new terms, in whole or in part, please stop using the website and the Service.</p>
<h2>Contact Us</h2>
<p>If you have any questions about these Terms and Conditions, You can contact us:</p>
<ul>
<li>By email: netanelazar880@gmail.com</li>
</ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
