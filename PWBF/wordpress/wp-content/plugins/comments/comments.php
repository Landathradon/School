<?php
/**
 * Plugin Name: Comments
 * Plugin URI: https://www.undefined.fr
 * Description: Cut comments to 35 char
 * Version: 1.0.0
 * Author Name: Jean-Maxime (hello@undefined.fr)
 * Author: Jean-Maxime (Undefined)
 * Domain Path: /languages
 * Text Domain: Comments
 * Author URI: https://www.undefined.fr/#about
 */

add_filter("comment_text", "FiltreText");

function FiltreText($content)
{
    return substr($content, 0, 35);
}

add_action('the_content', 'Ajout_Couleur');

function Ajout_Couleur($content)
{
    $source = array( 'salut', 'Salut' );
    $change = array('<span style="color:#b01b1b; font-weight: bold">salut</span>','<span style="color:#b01b1b; font-weight: bold">Salut</span>');
    return str_replace( $source, $change, $content );
}
